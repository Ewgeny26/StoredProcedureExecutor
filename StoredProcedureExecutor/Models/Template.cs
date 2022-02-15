using System;

namespace StoredProcedureExecutor.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public byte[]? DataFile { get; set; }
        public DateTime CreatedAt { get; set; }

        public Procedure? Procedure { get; set; }
        public int ProcedureId { get; set; }
    }
}
