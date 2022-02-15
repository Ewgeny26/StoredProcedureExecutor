using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Infrastructure;

namespace StoredProcedureExecutor.ViewModels
{
    public class ErrorDialogViewModel: ViewModelBase, IDialogViewModel
    {
        public ErrorDto? Error { get; set; }

        public void Initialize(object? model = null)
        {
            if(model is ErrorDto)
            {
                Error = (ErrorDto)model;
            }
        }
    }
}
