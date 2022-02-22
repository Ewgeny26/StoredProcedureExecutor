using StoredProcedureExecutor.ViewModels;
using System.Windows.Controls;

namespace StoredProcedureExecutor.Pages
{
    /// <summary>
    /// Interaction logic for ExecutingPage.xaml
    /// </summary>
    public partial class ExecutingPage : UserControl
    {
        public ExecutingPage(ExecutingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}