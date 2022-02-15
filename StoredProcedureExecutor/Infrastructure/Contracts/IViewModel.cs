using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Constracts
{
    public interface IViewModel
    {
        Task Initialize(Action? whenDone, object? model);
    }
}
