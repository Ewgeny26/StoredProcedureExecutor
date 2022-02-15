using System.Collections.Generic;

namespace StoredProcedureExecutor.Configurations
{
    public class DbConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Schema { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public int ConnectionTimeout { get; set; }
        public int CommandTimeout { get; set; }
        public int ConnectRetryCount { get; set; }
        public int ProcExecRetryCount { get; set; }
        public int ProcExecRetryDelay { get; set; }
        public List<string>? AvailabeleExecProcOnServers { get; set; }
    }
}

