using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Infrastructure.Contracts
{
    public interface IViewModel
    {
        Task Initialize(Action? whenDone, object? model);
    }
}