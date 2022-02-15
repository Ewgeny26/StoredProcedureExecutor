using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Infrastructure.Contracts;
using StoredProcedureExecutor.Infrastructure.Impementations;
using StoredProcedureExecutor.Services.Constracts;
using System;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddInfrastructureServicesHostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructureServices(this IHostBuilder host)
        {

            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigationService>(d => new NavigationService(d));
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IDialogsService>(d => new DialogsService(d));
                services.AddSingleton<IWindowNavigation>(d => d.GetRequiredService<MainWindow>());
                services.AddSingleton<IWindowSnackbars>(d => d.GetRequiredService<MainWindow>());
                services.AddSingleton<IGlobalExceptionHandlerService, GlobalExceptionHandlerService>();
            });
            return host;
        }
    }
}
