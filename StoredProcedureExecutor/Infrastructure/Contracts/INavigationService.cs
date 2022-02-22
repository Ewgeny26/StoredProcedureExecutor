using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Infrastructure.Contracts
{
    public interface INavigationService
    {
        Task NavigateTo<TViewModel>(Action? whenDone = null, object? model = null) where TViewModel : IViewModel;
    }
}