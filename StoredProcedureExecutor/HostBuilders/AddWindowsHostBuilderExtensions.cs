using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddWindowsHostBuilderExtensions
    {
        public static IHostBuilder AddWindows(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
            });
            return host;
        }
    }
}
