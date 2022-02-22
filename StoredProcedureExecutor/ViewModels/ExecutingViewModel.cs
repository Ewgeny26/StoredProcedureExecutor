using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.UICommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class ExecutingViewModel : ViewModelBase, IViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogsService _dialogsService;
        private readonly IProceduresService _proceduresService;
        private readonly ITemplatesService _templatesService;

        private Action? _whenDone;
        private string? _reportPath;
        private ProcedureDto? _procedure;
        private TemplateDto? _template;

        public ExecutingViewModel(
            IReportsService reportsService,
            IEmailSenderService emailSenderService,
            IProcExecutorService procExecutorService,
            ISnackbarService snackbarService,
            INavigationService navigationService,
            IDialogsService dialogsService,
            IProceduresService proceduresService,
            ITemplatesService templatesService,
            ICurrentUserService currentUserService,
            IExecStatisticsService execStatisticsService
        )
        {
            _navigationService = navigationService;
            _dialogsService = dialogsService;
            _proceduresService = proceduresService;
            _templatesService = templatesService;

            var saveProcedureCommand = new SaveProcedureCommand(this, proceduresService, currentUserService);
            var executeProcedureCommand =
                new ExecuteProcedureCommand(this, procExecutorService, snackbarService, execStatisticsService);
            var refreshTemplateCommand =
                new RefreshReportCommand(this, reportsService, snackbarService, execStatisticsService);
            var sendTemplateCommand =
                new SendReportCommand(this, emailSenderService, snackbarService, execStatisticsService);

            ExecuteProcedureCommand = new AsyncCompositeCommand(ExecuteLoader,
                new List<AsyncCommandBase> { saveProcedureCommand, executeProcedureCommand });
            RefreshDataTemplateCommand = new AsyncCompositeCommand(RefreshLoader,
                new List<AsyncCommandBase> { saveProcedureCommand, refreshTemplateCommand });
            SendTemplateToEmailCommand = new AsyncCompositeCommand(SendTemplateLoader,
                new List<AsyncCommandBase> { saveProcedureCommand, sendTemplateCommand });
            RunPipelineCommand = new AsyncCompositeCommand(PipelineLoader,
                new List<AsyncCommandBase>
                    { saveProcedureCommand, executeProcedureCommand, refreshTemplateCommand, sendTemplateCommand });

            EditCommand = new RelayCommand(EditProcedure);
            OpenFolderDialogCommand = new RelayCommand(OpenFolderDialog);
            BackCommand = new RelayCommand(Back);
        }

        public async Task Initialize(Action? whenDone, object? model)
        {
            _whenDone = whenDone ?? throw new ArgumentNullException(nameof(whenDone));
            if (model != null && int.TryParse(model.ToString(), out var procedureId))
            {
                Procedure = await _proceduresService.GetProcedureById(procedureId);
                var procedureParams = await _proceduresService.GetProcedureParams(procedureId);
                if (procedureParams != null)
                {
                    Params.FillObservableCollection(procedureParams);
                }

                Template = await _templatesService.GetByProcedureId(procedureId);
            }
            else
            {
                throw new ArgumentNullException(nameof(model));
            }
        }

        #region Commands

        public ICommand BackCommand { get; }
        public ICommand RefreshDataTemplateCommand { get; }
        public ICommand SendTemplateToEmailCommand { get; }
        public ICommand ExecuteProcedureCommand { get; }
        public ICommand RunPipelineCommand { get; }
        public ICommand OpenFolderDialogCommand { get; }
        public ICommand EditCommand { get; }

        #endregion

        #region Properties

        public ProcedureDto? Procedure
        {
            get => _procedure;
            set => SetProperty(ref _procedure, value);
        }

        public ObservableCollection<ParamDto> Params { get; } = new ObservableCollection<ParamDto>();

        public TemplateDto? Template
        {
            get => _template;
            set => SetProperty(ref _template, value);
        }

        public CommandLoader ExecuteLoader { get; } = new CommandLoader();
        public CommandLoader RefreshLoader { get; } = new CommandLoader();
        public CommandLoader SendTemplateLoader { get; } = new CommandLoader();
        public CommandLoader PipelineLoader { get; } = new CommandLoader();

        public bool IsOperationRun =>
            !ExecuteLoader.BtnEnable
            || !RefreshLoader.BtnEnable
            || !SendTemplateLoader.BtnEnable
            || !PipelineLoader.BtnEnable;

        public string? ReportPath
        {
            get => _reportPath;
            set => SetProperty(ref _reportPath, value);
        }

        #endregion

        #region Private Methods

        private void Back()
        {
            _whenDone?.Invoke();
        }

        private async void EditProcedure()
        {
            await _navigationService.NavigateTo<AddNewViewModel>(ReturnOnPage, Procedure?.Id);
        }

        private async void ReturnOnPage()
        {
            await _navigationService.NavigateTo<ExecutingViewModel>(_whenDone, Procedure?.Id);
        }

        private void OpenFolderDialog()
        {
            if (Procedure == null) return;
            Procedure.OutputReportPath = _dialogsService.ShowFolderDialog();
        }

        #endregion
    }
}