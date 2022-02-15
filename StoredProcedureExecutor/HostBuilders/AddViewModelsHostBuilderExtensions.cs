using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.ViewModels;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {

            host.ConfigureServices(services =>
            {
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<ProceduresViewModel>();
                services.AddTransient<AddNewViewModel>();
                services.AddTransient<ExecutingViewModel>();
                services.AddTransient<ConfirmDialogViewModel>();
                services.AddTransient<ErrorDialogViewModel>();
            });
            return host;
        }
    }
}
