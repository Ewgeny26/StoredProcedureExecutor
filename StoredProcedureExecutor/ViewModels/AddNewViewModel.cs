using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.UICommands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class AddNewViewModel : ViewModelBase, IViewModel
    {
        private readonly IDialogsService _dialogsService;
        private readonly ISnackbarService _snackbarService;
        private readonly IProceduresService _proceduresService;
        private readonly IProcExecutorService _procExecutorService;
        private readonly ITemplatesService _templatesService;

        private Action? _whenDone;
        private ParamDto? _selectedProcedureParam;
        private TemplateDto _template = new TemplateDto();
        private ProcedureDto _procedureDto = new ProcedureDto();
        private bool _isAddingMode = true;

        public AddNewViewModel(
            IDialogsService dialogsService,
            ISnackbarService snackbarService,
            IProceduresService proceduresService,
            IProcExecutorService procExecutorService,
            ITemplatesService templatesService)
        {
            _dialogsService = dialogsService;
            _snackbarService = snackbarService;
            _proceduresService = proceduresService;
            _procExecutorService = procExecutorService;
            _templatesService = templatesService;

            AddProcedureCommand = new AsyncRelayCommand(AddProcedure, AddProcedureLoader, CanAddingOrUpdatingProcedure);
            UpdateProcedureCommand =
                new AsyncRelayCommand(UpdateProcedure, UpdateProcedureLoader, CanAddingOrUpdatingProcedure);
            LoadProcedureParamsCommand = new AsyncRelayCommand(LoadProcedureParams, ParamsLoader, CanLoadingParams);
            SaveTemplateDialogCommand = new AsyncRelayCommand(DownloadTemplate, canExecute: CanDownloadingTemplate);

            CancelCommand = new RelayCommand(Cancel);
            RemoveParamCommand = new RelayCommand(RemoveParam, () => _selectedProcedureParam != null);
            UploadFileDialogCommand = new RelayCommand(OpenFileDialog);
        }

        public async Task Initialize(Action? whenDone, object? model)
        {
            _whenDone = whenDone;
            if (model != null && int.TryParse(model.ToString(), out var procedureId))
            {
                var procedure = await _proceduresService.GetProcedureById(procedureId);
                var procedureParams = await _proceduresService.GetProcedureParams(procedureId);
                ProcedureDto = procedure ?? new ProcedureDto();
                Params.FillObservableCollection(procedureParams);
                var template = await _templatesService.GetByProcedureId(procedureId);
                Template = template ?? new TemplateDto();
                Template.TemplatePath = Template.Name;
                IsAddingMode = false;
            }

            AvailableServers.FillObservableCollection(_procExecutorService.GetAvailableServers());
        }

        #region Commands

        public ICommand CancelCommand { get; }
        public ICommand RemoveParamCommand { get; }
        public ICommand AddProcedureCommand { get; }
        public ICommand UpdateProcedureCommand { get; }
        public ICommand LoadProcedureParamsCommand { get; }
        public ICommand UploadFileDialogCommand { get; }
        public ICommand SaveTemplateDialogCommand { get; }

        #endregion

        #region Properties

        public ProcedureDto ProcedureDto
        {
            get => _procedureDto;
            set => SetProperty(ref _procedureDto, value);
        }

        public ObservableCollection<ParamDto> Params { get; } = new ObservableCollection<ParamDto>();
        public ObservableCollection<string> AvailableServers { get; } = new ObservableCollection<string>();

        public ParamDto? SelectedProcedureParam
        {
            get => _selectedProcedureParam;
            set => SetProperty(ref _selectedProcedureParam, value);
        }

        public CommandLoader ParamsLoader { get; } = new CommandLoader();
        public CommandLoader AddProcedureLoader { get; } = new CommandLoader();
        public CommandLoader UpdateProcedureLoader { get; } = new CommandLoader();

        public TemplateDto Template
        {
            get => _template;
            set => SetProperty(ref _template, value);
        }

        public bool IsAddingMode
        {
            get => _isAddingMode;
            set => SetProperty(ref _isAddingMode, value);
        }

        #endregion

        #region Private Methods

        private void Cancel()
        {
            _whenDone?.Invoke();
        }

        private void RemoveParam()
        {
            if (_selectedProcedureParam != null)
            {
                Params.Remove(_selectedProcedureParam);
            }
        }

        private async Task AddProcedure()
        {
            if (string.IsNullOrWhiteSpace(Template.TemplatePath))
            {
                return;
            }

            await _procExecutorService.CheckExistProcedure(ProcedureDto);
            _templatesService.CheckExistTemplate(Template.TemplatePath);
            var procedure = await _proceduresService.CreateProcedureAsync(ProcedureDto, Params);
            await _templatesService.Upload(Template.TemplatePath, procedure.Id!.Value);
            _snackbarService.Success(StatusMessages.ProcedureCreated);
            _whenDone?.Invoke();
        }

        private async Task LoadProcedureParams()
        {
            await _procExecutorService.CheckExistProcedure(ProcedureDto);
            var procedureParams = await _procExecutorService.GetProcedureParamsInfo(ProcedureDto);
            Params.Clear();
            foreach (var param in procedureParams)
            {
                var createParam = new ParamDto { Name = param.Name, Alias = param.Name, Type = param.Type };
                Params.Add(createParam);
            }

            _snackbarService.Success(StatusMessages.ProcedureParamsLoaded);
        }

        private bool CanAddingOrUpdatingProcedure()
        {
            return CanLoadingParams()
                   && !Params.Any(p => string.IsNullOrWhiteSpace(p.Alias));
        }

        private bool CanLoadingParams()
        {
            return !string.IsNullOrWhiteSpace(ProcedureDto.Schema)
                   && !string.IsNullOrWhiteSpace(ProcedureDto.Name)
                   && !string.IsNullOrWhiteSpace(Template.TemplatePath)
                   && ParamsLoader.Loader != Visibility.Visible;
        }

        private void OpenFileDialog()
        {
            Template.TemplatePath = _dialogsService.ShowFileDialog(FileDialogFilter.Excel);
        }

        private async Task UpdateProcedure()
        {
            await _procExecutorService.CheckExistProcedure(ProcedureDto);
            if (Template.TemplatePath != Template.Name)
            {
                _templatesService.CheckExistTemplate(Template.TemplatePath);
                if (Template.Id != null)
                {
                    await _templatesService.Remove(Template.Id.Value);
                }

                await _templatesService.Upload(Template.TemplatePath, ProcedureDto.Id!.Value);
            }

            await _proceduresService.UpdateProcedure(ProcedureDto, Params);
            _snackbarService.Success(StatusMessages.ProcedureUpdated);
            _whenDone?.Invoke();
        }

        private async Task DownloadTemplate()
        {
            var saveTemplatePath = _dialogsService.ShowSaveDialog(Template.Name, FileDialogFilter.Excel);
            if (!string.IsNullOrWhiteSpace(saveTemplatePath) && Template?.Id != null)
            {
                var outputPath = Path.GetDirectoryName(saveTemplatePath) ??
                                 throw new InvalidPathException($"Invalid path [{saveTemplatePath}]");
                var fileName = Path.GetFileNameWithoutExtension(saveTemplatePath);
                await _templatesService.DownloadFileAsync(Template, fileName, outputPath);
            }
        }

        private bool CanDownloadingTemplate()
        {
            return Template?.Id != null;
        }

        #endregion
    }
}