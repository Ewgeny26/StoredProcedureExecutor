using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Infrastructure.Implementations;

namespace StoredProcedureExecutor.Dtos
{
    public class ErrorDto : NotifyPropertyChangedBase
    {
        private string _message = string.Empty;
        private string? _details;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string? Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }
    }
}