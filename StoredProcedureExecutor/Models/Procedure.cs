using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;

namespace StoredProcedureExecutor.Models
{
    public class Procedure : IProcedureInfo
    {
        public int Id { get; set; }
        public string Server { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Schema { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastExecutedAt { get; set; }
        public DateTime? LastRefreshedAt { get; set; }
        public DateTime? LastSentTemplateAt { get; set; }
        public string? EmailRecipients { get; set; }
        public string? EmailSubject { get; set; }
        public string? OutputReportPath { get; set; }
        public string? LastUsername { get; set; }

        public Template? Template { get; set; }
        public List<ProcedureParam>? Params { get; set; }

        public Procedure() { }
        public Procedure(string server, string database, string schema, string name, string? description = null)
        {
            Database = database;
            Server = server;
            Schema = schema;
            Name = name;
            Description = description;
        }
    }
}
