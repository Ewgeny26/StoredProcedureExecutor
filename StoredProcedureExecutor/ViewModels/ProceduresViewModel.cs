using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.UICommands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class ProceduresViewModel : ViewModelBase, IViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IProceduresService _proceduresService;
        private readonly IDialogsService _dialogsService;
        private readonly ISnackbarService _snackbarService;

        private ProcedureDto? _selectedProcedure;
        public ProceduresViewModel(
            INavigationService navigationService,
            IProceduresService proceduresService,
            IDialogsService dialogsService,
            ISnackbarService snackbarService)
        {
            _navigationService = navigationService;
            _proceduresService = proceduresService;
            _dialogsService = dialogsService;
            _snackbarService = snackbarService;

            AddNewProcedureCommand = new AsyncRelayCommand(AddNewProcedure);
            ExecuteProcedureCommand = new AsyncRelayCommand(ExecuteProcedure, canExecute: CanOperation);
            RemoveProcedureCommand = new AsyncRelayCommand(RemoveProcedure, RemoveLoader, CanOperation);
            EditProcedureCommand = new AsyncRelayCommand(EditProcedure, canExecute: CanOperation);

        }
        public async Task Initialize(Action? whenDone, object? model)
        {
            Procedures.FillObservableCollection(await _proceduresService.GetProcedures());
        }

        #region Commands
        public ICommand AddNewProcedureCommand { get; }
        public ICommand ExecuteProcedureCommand { get; }
        public ICommand RemoveProcedureCommand { get; }
        public ICommand EditProcedureCommand { get; }

        #endregion

        #region Properties
        public ObservableCollection<ProcedureDto> Procedures { get; } = new ObservableCollection<ProcedureDto>();
        public ProcedureDto? SelectedProcedure
        {
            get => _selectedProcedure;
            set => SetProperty(ref _selectedProcedure, value);
        }
        public CommandLoader RemoveLoader { get; } = new CommandLoader();
        #endregion

        #region Private Methods
        private async Task AddNewProcedure()
        {
            await _navigationService.NavigateTo<AddNewViewModel>(GoBack, null);
        }

        private async Task RemoveProcedure()
        {
            if (SelectedProcedure == null || SelectedProcedure.Id == null) return;
            var answer = await _dialogsService.ShowConfirmDialog(ConfirmMessages.RemoveProcedure);
            if (answer == true)
            {
                await _proceduresService.RemoveProcedure(SelectedProcedure.Id.Value);
                Procedures.Remove(SelectedProcedure);
                _snackbarService.Success(StatusMessages.ProcedureDeleted);
            }
        }

        private async Task EditProcedure()
        {
            await _navigationService.NavigateTo<AddNewViewModel>(GoBack, SelectedProcedure?.Id);
        }

        private async Task ExecuteProcedure()
        {
            if (_selectedProcedure == null) return;
            await _navigationService.NavigateTo<ExecutingViewModel>(GoBack, _selectedProcedure.Id);
        }

        private async void GoBack()
        {
            await _navigationService.NavigateTo<ProceduresViewModel>();
        }

        private bool CanOperation()
        {
            return _selectedProcedure != null;
        }

        #endregion
    }
}
