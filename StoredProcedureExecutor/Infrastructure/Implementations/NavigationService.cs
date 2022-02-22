using Microsoft.Extensions.DependencyInjection;
using StoredProcedureExecutor.Pages;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.Infrastructure.Implementations
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task NavigateTo<TViewModel>(Action? whenDone = null, object? model = null)
            where TViewModel : IViewModel
        {
            var mainWindow = _serviceProvider.GetRequiredService<IWindowNavigation>();
            mainWindow.Main.Children.Clear();
            mainWindow.Loader.Visibility = Visibility.Visible;
            try
            {
                var page = this.CreatePage(typeof(TViewModel));
                var viewModel = page.DataContext as IViewModel
                                ?? throw new Exception($"Unknown view model: {typeof(TViewModel)}.");
                await viewModel.Initialize(whenDone, model);
                mainWindow.Main.Children.Add(page);
            }
            finally
            {
                mainWindow.Loader.Visibility = Visibility.Hidden;
                CommandManager.InvalidateRequerySuggested(); // Refresh all binding commands
            }
        }


        private UserControl CreatePage(Type viewModelType)
        {
            if (viewModelType == typeof(ProceduresViewModel))
            {
                return _serviceProvider.GetRequiredService<ProceduresPage>();
            }
            else if (viewModelType == typeof(AddNewViewModel))
            {
                return _serviceProvider.GetRequiredService<AddNewPage>();
            }
            else if (viewModelType == typeof(ExecutingViewModel))
            {
                return _serviceProvider.GetRequiredService<ExecutingPage>();
            }
            else
            {
                throw new Exception($"Unknown view model: {viewModelType}.");
            }
        }
    }
}