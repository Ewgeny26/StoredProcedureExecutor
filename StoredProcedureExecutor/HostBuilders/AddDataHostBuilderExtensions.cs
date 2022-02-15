using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Data;
using StoredProcedureExecutor.Services.Contracts;
using StoredProcedureExecutor.Services.Implementations;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddDataHostBuilderExtensions
    {
        public static IHostBuilder AddData(this IHostBuilder host)
        {

            host.ConfigureServices(services =>
            {
                services.AddTransient<IProceduresDbContext, ProceduresDbContext>();
                services.AddTransient<IPlainQueryExecutorService, MSSQLQueryExecutorService>();
            });
            return host;
        }
    }
}
