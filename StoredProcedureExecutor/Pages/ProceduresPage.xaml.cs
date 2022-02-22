using StoredProcedureExecutor.ViewModels;
using System.Windows.Controls;

namespace StoredProcedureExecutor.Pages
{
    /// <summary>
    /// Interaction logic for ProceduresPage.xaml
    /// </summary>
    public partial class ProceduresPage : UserControl
    {
        public ProceduresPage(ProceduresViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}