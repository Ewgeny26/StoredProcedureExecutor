using System;

namespace StoredProcedureExecutor.Models
{
    public class CloudFile: CloudEntity
    {
        public string Url { get; set; } = string.Empty;
        public string? Title { get; set; }
    }
}
