using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.UICommands
{
    public class ExecuteProcedureCommand : AsyncCommandBase
    {
        protected readonly IProcExecutorService _procExecutorService;
        protected readonly ISnackbarService _snackbarService;
        protected readonly IExecStatisticsService _execStatisticsService;
        protected readonly ExecutingViewModel _viewModel;
        public ExecuteProcedureCommand(
            ExecutingViewModel viewModel,
            IProcExecutorService procExecutorService,
            ISnackbarService snackbarService,
            IExecStatisticsService execStatisticsService)
        {
            _procExecutorService = procExecutorService;
            _snackbarService = snackbarService;
            _execStatisticsService = execStatisticsService;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_viewModel.Procedure == null) return;
            var timer = new TimerDto();
            try
            {
                timer.Start();
                await _procExecutorService.ExecuteProc(_viewModel.Procedure, _viewModel.Params);
                _viewModel.Procedure.LastExecutedAt = DateTime.UtcNow;
                _snackbarService.Success(StatusMessages.ProcedereExecuted);
            }
            finally 
            {
                timer.Stop();
                await _execStatisticsService.SaveProcedureExecuteInfo(timer, _viewModel.Procedure, _viewModel.Params);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (!_viewModel.Params?.ToList().Any(param => param.Value == null) ?? true) && !_viewModel.IsOperationRun;
        }
    }
}
