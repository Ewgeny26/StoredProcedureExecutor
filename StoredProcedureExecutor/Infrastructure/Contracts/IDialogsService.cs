using StoredProcedureExecutor.Dtos;
using System.Threading.Tasks;
namespace StoredProcedureExecutor.Infrastructure
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
