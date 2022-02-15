using StoredProcedureExecutor.ViewModels;
using System.Windows.Controls;

namespace StoredProcedureExecutor.Pages.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : UserControl
    {
        public ConfirmDialog(ConfirmDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
