using StoredProcedureExecutor.Dtos;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IEmailSenderService
    {
        Task SendAsync(EmailMessageDto message);
    }
}
