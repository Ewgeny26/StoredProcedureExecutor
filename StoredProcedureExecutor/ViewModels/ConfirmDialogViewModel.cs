using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class ConfirmDialogViewModel : ViewModelBase, IDialogViewModel
    {
        public ConfirmDialogViewModel()
        {
            Text = string.Empty;
        }

        public string Text { get; private set; }

        public void Initialize(object? model = null)
        {
            if (model == null) return;
            Text = (string)model;
        }
    }
}