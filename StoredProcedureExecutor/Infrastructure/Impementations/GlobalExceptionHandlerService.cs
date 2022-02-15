using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Infrastructure.Contracts;
using System;

namespace StoredProcedureExecutor.Infrastructure.Impementations
{
    public class GlobalExceptionHandlerService : IGlobalExceptionHandlerService
    {
        private readonly IDialogsService _dialogsService;
        public GlobalExceptionHandlerService(IDialogsService dialogsService)
        {
            _dialogsService = dialogsService;
        }
        public async void Handle(Exception? exception)
        {
            var errorDto = new ErrorDto();
            switch (exception)
            {
                case BusinessLogicException _:
                    errorDto.Message = exception.Message;
                    break;
                case RetryLimitExceededException _:
                    errorDto.Message = exception.Message;
                    break;
                case SqlException _:
                    errorDto.Message = exception.Message;
                    break;
                default:
                    errorDto.Message = exception?.Message ?? ErrorMessages.Common;
                    break;
            }
            await _dialogsService.ShowErrorDialog(errorDto);
        }
    }
}
