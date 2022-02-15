using StoredProcedureExecutor.ViewModels;
using System.Windows.Controls;

namespace StoredProcedureExecutor.Pages.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : UserControl
    {
        public ErrorDialog(ErrorDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
