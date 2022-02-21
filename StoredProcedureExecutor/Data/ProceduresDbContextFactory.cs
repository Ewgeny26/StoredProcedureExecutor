using Microsoft.EntityFrameworkCore.Design;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Constants;
using System;

namespace StoredProcedureExecutor.Data
{
    public class ProceduresDbContextFactory : IDesignTimeDbContextFactory<ProceduresDbContext>
    {
        private const int MIN_ARGS_COUNT = 2;
        private const int ENV_VARIABLE_NAME_INDEX = 0;
        private const int ENV_VARIABLE_VALUE_INDEX = 1;
        public ProceduresDbContext CreateDbContext(string[] args)
        {
            var environment = GetEnvironment(args) ?? throw new ArgumentNullException("Input param [--environment] must not be empty");
            var configuration = new ConfigurationFactory(environment).CreateRequired<DbConfiguration>(ConfigSection.Database);
            return new ProceduresDbContext(configuration);
        }

        private static string? GetEnvironment(string[] args)
        {
            if (args.Length < MIN_ARGS_COUNT)
            {
                return null;
            }
            var command = args[ENV_VARIABLE_NAME_INDEX];
            if (command.Contains("--environment"))
            {
                return args[ENV_VARIABLE_VALUE_INDEX];
            }
            else
            {
                return null;
            }
        }
    }
}
