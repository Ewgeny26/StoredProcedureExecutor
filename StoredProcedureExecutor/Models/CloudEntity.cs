using System;

namespace StoredProcedureExecutor.Models
{
    public abstract class CloudEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ServerRelativeUrl { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
