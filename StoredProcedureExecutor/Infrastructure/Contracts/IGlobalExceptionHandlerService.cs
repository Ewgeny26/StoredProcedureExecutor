using System;

namespace StoredProcedureExecutor.Infrastructure.Contracts
{
    public interface IGlobalExceptionHandlerService
    {
        void Handle(Exception? exception);
    }
}
