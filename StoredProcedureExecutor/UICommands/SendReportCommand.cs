using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.UICommands
{
    public class SendReportCommand : AsyncCommandBase
    {
        protected readonly IEmailSenderService _emailSenderService;
        protected readonly ISnackbarService _snackbarService;
        protected readonly IExecStatisticsService _execStatisticsService;
        protected readonly ExecutingViewModel _viewModel;

        public SendReportCommand(
            ExecutingViewModel viewModel,
            IEmailSenderService emailSenderService,
            ISnackbarService snackbarService,
            IExecStatisticsService execStatisticsService)
        {
            _viewModel = viewModel;
            _emailSenderService = emailSenderService;
            _execStatisticsService = execStatisticsService;
            _snackbarService = snackbarService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_viewModel.Procedure?.EmailRecipients == null || _viewModel.Procedure.EmailSubject == null ||
                _viewModel.ReportPath == null)
            {
                return;
            }

            var timer = new TimerDto();
            try
            {
                timer.Start();
                var attachments = new List<string> { _viewModel.ReportPath };
                var emailMessage = new EmailMessageDto(_viewModel.Procedure.EmailRecipients.Split(";"),
                    _viewModel.Procedure.EmailSubject, null, attachments);
                await _emailSenderService.SendAsync(emailMessage);
                _viewModel.Procedure.LastSentTemplateAt = DateTime.UtcNow;
                _snackbarService.Success(StatusMessages.TemplateSent);
            }
            finally
            {
                timer.Stop();
                await _execStatisticsService.SaveReportSendInfo(timer, _viewModel.Procedure);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel?.Procedure?.EmailRecipients)
                   && !string.IsNullOrWhiteSpace(_viewModel?.Procedure?.EmailSubject)
                   && !_viewModel.IsOperationRun;
        }
    }
}