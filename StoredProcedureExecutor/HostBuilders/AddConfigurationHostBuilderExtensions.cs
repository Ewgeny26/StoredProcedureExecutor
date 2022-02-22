using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Constants;

namespace StoredProcedureExecutor.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                var configurationFactory = new ConfigurationFactory();
                services.AddSingleton(configurationFactory);
                services.AddTransient(d =>
                    configurationFactory.CreateRequired<DbConfiguration>(ConfigSection.Database));
                services.AddTransient(d =>
                    configurationFactory.CreateRequired<EmailConfiguration>(ConfigSection.Email));
            });
            return host;
        }
    }
}