using System.Threading.Tasks;
using StoredProcedureExecutor.Dtos;

namespace StoredProcedureExecutor.Infrastructure.Contracts
{
    public interface IDialogsService
    {
        Task<TResult?> Show<TViewModel, TResult>(object? model = null) where TViewModel : IDialogViewModel;
        void Close<TResult>(TResult? result);
        string ShowFileDialog(string? filter = null);
        string ShowFolderDialog(string? filter = null);
        Task ShowErrorDialog(ErrorDto error);
        Task<bool> ShowConfirmDialog(string message);
        string ShowSaveDialog(string? fileName, string? filter);
    }
}