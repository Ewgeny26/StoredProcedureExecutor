using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class ErrorDialogViewModel : ViewModelBase, IDialogViewModel
    {
        public ErrorDto? Error { get; set; }

        public void Initialize(object? model = null)
        {
            if (model is ErrorDto dto)
            {
                Error = dto;
            }
        }
    }
}