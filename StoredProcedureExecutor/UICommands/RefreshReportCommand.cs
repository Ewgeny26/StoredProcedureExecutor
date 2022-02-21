using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.UICommands
{
    public class RefreshReportCommand : AsyncCommandBase
    {
        protected readonly IReportsService _reportsService;
        protected readonly ISnackbarService _snackbarService;
        protected readonly IExecStatisticsService _execStatisticsService;
        protected readonly ExecutingViewModel _viewModel;
        public RefreshReportCommand(
            ExecutingViewModel viewModel,
            IReportsService reportsService,
            ISnackbarService snackbarService,
            IExecStatisticsService execStatisticsService)
        {
            _reportsService = reportsService;
            _snackbarService = snackbarService;
            _execStatisticsService = execStatisticsService;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_viewModel.Template == null || string.IsNullOrWhiteSpace(_viewModel.Procedure?.OutputReportPath)) return;
            var timer = new TimerDto();
            try
            {
                timer.Start();
                _viewModel.ReportPath = await _reportsService.CreateReportByTemplate(_viewModel.Template, _viewModel.Procedure.OutputReportPath, _viewModel.Params);
                _viewModel.Procedure.LastRefreshedAt = DateTime.UtcNow;
                _snackbarService.Success(StatusMessages.TemplateRefreshed);

            }
            finally
            {
                timer.Stop();
                await _execStatisticsService.SaveReportRefreshInfo(timer, _viewModel.Procedure, _viewModel.Template.Name);
            }

        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.Template != null
                && !string.IsNullOrWhiteSpace(_viewModel.Procedure?.OutputReportPath)
                && !_viewModel.IsOperationRun;
        }
    }
}
