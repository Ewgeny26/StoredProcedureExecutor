using System;
using System.Data;

namespace StoredProcedureExecutor.Models
{
    public class ProcedureParam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SqlDbType Type { get; set; }
        public string? Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }

        public ProcedureParam() { }
        public ProcedureParam(string name, SqlDbType type, Procedure procedure)
        {
            Name = name;
            Type = type;
            Procedure = procedure;
        }
    }
}
