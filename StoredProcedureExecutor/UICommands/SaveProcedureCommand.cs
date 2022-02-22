using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.ViewModels;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.UICommands
{
    public class SaveProcedureCommand : AsyncCommandBase
    {
        protected readonly IProceduresService _procedureService;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly ExecutingViewModel _viewModel;

        public SaveProcedureCommand(
            ExecutingViewModel executingViewModel,
            IProceduresService proceduresService,
            ICurrentUserService currentUserService)
        {
            _procedureService = proceduresService;
            _viewModel = executingViewModel;
            _currentUserService = currentUserService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_viewModel.Procedure == null)
            {
                return;
            }
            _viewModel.Procedure.LastUsername = _currentUserService.GetUsername();
            await _procedureService.UpdateProcedure(_viewModel.Procedure, _viewModel.Params);
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }
    }
}