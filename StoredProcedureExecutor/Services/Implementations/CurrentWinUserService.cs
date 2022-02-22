using StoredProcedureExecutor.Services.Contracts;
using System.Security.Principal;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class CurrentWinUserService : ICurrentUserService
    {
        public string? GetUsername()
        {
            return WindowsIdentity.GetCurrent().Name;
        }
    }
}