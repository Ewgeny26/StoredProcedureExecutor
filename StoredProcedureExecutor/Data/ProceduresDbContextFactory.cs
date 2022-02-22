using Microsoft.EntityFrameworkCore.Design;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Constants;
using System;

namespace StoredProcedureExecutor.Data
{
    public class ProceduresDbContextFactory : IDesignTimeDbContextFactory<ProceduresDbContext>
    {
        private const int MinArgsCount = 2;
        private const string EnvVariableName = "--environment";
        private const int EnvVariableNameIndex = 0;
        private const int EnvVariableValueIndex = 1;

        public ProceduresDbContext CreateDbContext(string[] args)
        {
            var environment = GetEnvironment(args) ??
                              throw new ArgumentNullException($"Input param [{EnvVariableName}] must not be empty");
            var configuration =
                new ConfigurationFactory(environment).CreateRequired<DbConfiguration>(ConfigSection.Database);
            return new ProceduresDbContext(configuration);
        }

        private static string? GetEnvironment(string[] args)
        {
            if (args.Length < MinArgsCount)
            {
                return null;
            }

            var command = args[EnvVariableNameIndex];
            return command.Contains(EnvVariableName) ? args[EnvVariableValueIndex] : null;
        }
    }
}