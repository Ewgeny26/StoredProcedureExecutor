using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Constracts
{
    public interface INavigationService
    {
        Task NavigateTo<TViewModel>(Action? whenDone = null, object? model = null) where TViewModel : IViewModel;
    }
}
