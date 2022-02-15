using Microsoft.EntityFrameworkCore.Design;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Constants;
using System;

namespace StoredProcedureExecutor.Data
{
    internal class ProceduresDbContextFactory : IDesignTimeDbContextFactory<ProceduresDbContext>
    {
        public ProceduresDbContext CreateDbContext(string[] args)
        {
            var enviroment = GetEnvironment(args) ?? throw new ArgumentNullException("Input param [--environment] must not be empty");
            var configuration = new ConfigurationFactory(enviroment).CreateRequired<DbConfiguration>(ConfigSection.Database);
            return new ProceduresDbContext(configuration);
        }

        private string? GetEnvironment(string[] args)
        {
            if (args.Length < 2) return null;
            var command = args[0];
            if (command.Contains("--environment"))
            {
                return args[1];
            }
            else
            {
                return null;
            }
        }
    }
}
