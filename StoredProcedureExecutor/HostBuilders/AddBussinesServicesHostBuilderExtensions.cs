﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.Services.Implementations;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddBussinesServicesHostBuilderExtensions
    {
        public static IHostBuilder AddBussinesServices(this IHostBuilder host)
        {

            host.ConfigureServices(services =>
            {
                services.AddTransient<IProcExecutorService, ProcExecutorService>();
                services.AddTransient<IEmailSenderService, EmailSenderService>();
                services.AddTransient<ITemplateProviderService, ExcelTemplateProviderService>();
                //services.AddTransient<IFileCloudService, SharePointService>();
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
