using System.Collections.Generic;
using System.IO;

namespace StoredProcedureExecutor.Dtos
{
    public class EmailMessageDto
    {
        public IEnumerable<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string? Body { get; set; }
        public IEnumerable<string>? Attachments { get; set; }

        public EmailMessageDto(IEnumerable<string> recipients, string subject, string? body = null, IEnumerable<string>? attachments = null)
        {
            Recipients = recipients;
            Subject = subject;
            Body = body;
            Attachments = attachments;
        }
    }
}
