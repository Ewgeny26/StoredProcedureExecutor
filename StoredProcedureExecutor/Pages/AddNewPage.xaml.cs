using StoredProcedureExecutor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoredProcedureExecutor.Pages
{
    /// <summary>
    /// Interaction logic for AddNewPage.xaml
    /// </summary>
    public partial class AddNewPage : UserControl
    {
        public AddNewPage(AddNewViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
