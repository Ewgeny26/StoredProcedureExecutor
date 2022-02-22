using MaterialDesignThemes.Wpf;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowNavigation, IWindowSnackbars
    {
        public Grid Main => main;

        public Snackbar Snackbar => snackbar;

        public ProgressBar Loader => loader;

        public MainWindow(MainWindowViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            InitializeComponent();
            DataContext = viewModel;
        }


        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}

