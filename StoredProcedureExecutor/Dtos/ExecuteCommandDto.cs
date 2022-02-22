using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace StoredProcedureExecutor.Dtos
{
    public class ExecuteCommandDto
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public int RetryCount { get; set; }
        public int RetryDelay { get; set; }
        public List<SqlParameter>? Parameters { get; set; }
    }
}