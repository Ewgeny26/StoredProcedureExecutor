using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.HostBuilders;
using StoredProcedureExecutor.Infrastructure.Contracts;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace StoredProcedureExecutor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .AddConfiguration()
                .AddData()
                .AddBusinessServices()
                .AddViewModels()
                .AddPages()
                .AddInfrastructureServices()
                .AddWindows();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            GlobalExceptionHandlerRegistration();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            var navigator = _host.Services.GetRequiredService<INavigationService>();
            mainWindow.Show();
            await navigator.NavigateTo<ProceduresViewModel>();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private void GlobalExceptionHandlerRegistration()
        {
            Exception? exception = null;
            DispatcherUnhandledException += (sender, e) =>
            {
                e.Handled = true;
                _host.Services.GetRequiredService<IGlobalExceptionHandlerService>().Handle(e.Exception);
            };
            this.Dispatcher.UnhandledException += (sender, e) =>
            {
                e.Handled = true;
                _host.Services.GetRequiredService<IGlobalExceptionHandlerService>().Handle(e.Exception);
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                exception = e.Exception;
                _host.Services.GetRequiredService<IGlobalExceptionHandlerService>().Handle(exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                _host.Services.GetRequiredService<IGlobalExceptionHandlerService>()
                    .Handle(e.ExceptionObject as Exception);
            };
        }
    }
}