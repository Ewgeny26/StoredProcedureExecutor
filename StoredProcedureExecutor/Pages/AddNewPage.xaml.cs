using StoredProcedureExecutor.ViewModels;
using System.Windows.Controls;

namespace StoredProcedureExecutor.Pages
{
    /// <summary>
    /// Interaction logic for AddNewPage.xaml
    /// </summary>
    public partial class AddNewPage : UserControl
    {
        public AddNewPage(AddNewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}