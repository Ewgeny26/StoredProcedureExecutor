using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Pages;
using StoredProcedureExecutor.Pages.Dialogs;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddPagesHostBuilderExtensions
    {
        public static IHostBuilder AddPages(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<ProceduresPage>();
                services.AddTransient<AddNewPage>();
                services.AddTransient<ExecutingPage>();
                services.AddTransient<ConfirmDialog>();
                services.AddTransient<ErrorDialog>();
            });
            return host;
        }
    }
}