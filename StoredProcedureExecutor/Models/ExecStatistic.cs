using StoredProcedureExecutor.Enums;
using System;

namespace StoredProcedureExecutor.Models
{
    public class ExecStatistic
    {
        public int Id { get; set; }
        public string Procedure { get; set; } = string.Empty;
        public OperationType OperationType { get; set; }
        public string? Username { get; set; }
        public DateTime StartAt { get; init; }
        public TimeSpan TimeElpsed { get; init; }
        public string? ParamsJson { get; set; }

    }
}
