using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.Services.Implementations;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddBusinessServicesHostBuilderExtensions
    {
        public static IHostBuilder AddBusinessServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<IProcExecutorService, ProcExecutorService>();
                services.AddTransient<IEmailSenderService, EmailSenderService>();
                services.AddTransient<ITemplateProviderService, ExcelTemplateProviderService>();
                services.AddTransient<IProceduresService, ProceduresService>();
                services.AddTransient<IReportsService, ReportsService>();
                services.AddTransient<ITemplatesService, DbTemplatesService>();
                services.AddTransient<IExecStatisticsService, ExecStatisticsService>();
                services.AddSingleton<ICurrentUserService, CurrentWinUserService>();
            });
            return host;
        }
    }
}