using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Services.Contracts;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private const int MaxAttachmentLength = 30720; // 30mb
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSenderService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public async Task SendAsync(EmailMessageDto message)
        {
            using (var smtpClient = BuildSmtpClient())
            {
                using (var mail = new MailMessage())
                {
                    mail.Subject = message.Subject;
                    mail.Body = message.Body;
                    mail.From = new MailAddress(_emailConfiguration.From);
                    foreach (var recipient in message.Recipients)
                    {
                        mail.To.Add(new MailAddress(recipient));
                    }
                    if (message.Attachments != null)
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            // MailMessage execute Dispose for each file attachment
                            var file = File.OpenRead(attachment);
                            if (file.Length > MaxAttachmentLength)
                            {
                                await file.DisposeAsync();
                                throw new AccededSizeEmailAttachmentException($"File [{Path.GetFileName(attachment)}] acceded limit of size email attachment");
                            }
                            mail.Attachments.Add(new Attachment(file, Path.GetFileName(file.Name)));
                        }
                    }
                    await smtpClient.SendMailAsync(mail);
                }
            }

        }

        private SmtpClient BuildSmtpClient()
        {
            return new SmtpClient
            {
                Host = _emailConfiguration.SmtpServer,
                Port = _emailConfiguration.Port,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password),
                UseDefaultCredentials = false
            };
        }
    }
}
