using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Pages.Dialogs;
using StoredProcedureExecutor.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoredProcedureExecutor.Infrastructure.Impementations
{
    public class DialogsService : IDialogsService
    {
        private readonly IServiceProvider _serviceProvider;
        public DialogsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Close<TResult>(TResult? result)
        {
            DialogHost.Close("RootDialog", result);

        }

        public async Task<TResult?> Show<TViewModel, TResult>(object? model = null) where TViewModel : IDialogViewModel
        {
            var dialog = this.CreateDialog(typeof(TViewModel));
            var viewModel = dialog.DataContext as IDialogViewModel
               ?? throw new Exception($"Unknown view model: {typeof(IDialogViewModel)}.");
            viewModel.Initialize(model);
            var result = await DialogHost.Show(dialog, "RootDialog");
            return (TResult?)result;
        }

        public async Task<bool> ShowConfirmDialog(string message)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            return await mainWindow.Dispatcher.Invoke(async () =>
            {
                return await Show<ConfirmDialogViewModel, bool>(message);
            });
        }

        public async Task ShowErrorDialog(ErrorDto error)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            await mainWindow.Dispatcher.InvokeAsync(async () =>
           {
               var session = DialogHost.GetDialogSession("RootDialog");
               if (session != null && !session.IsEnded)
               {
                   return;
               }
               var dialog = this.CreateDialog(typeof(ErrorDialogViewModel));
               var viewModel = dialog.DataContext as IDialogViewModel
                 ?? throw new Exception($"Unknown view model: {typeof(IDialogViewModel)}.");
               viewModel.Initialize(error);
               await DialogHost.Show(dialog, "RootDialog");
           });
        }

        public string ShowFileDialog(string? filter = null)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.Filter = filter;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;
            }

            return string.Empty;
        }

        public string ShowFolderDialog(string? filter = null)
        {
            var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }
            return string.Empty;
        }

        public string ShowSaveDialog(string? fileName, string? filter)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.CheckPathExists = true;
            saveDialog.Filter = filter;
            saveDialog.FileName = fileName;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                return saveDialog.FileName;
            }

            return string.Empty;
        }

        private System.Windows.Controls.UserControl CreateDialog(Type viewModelType)
        {
            if (viewModelType == typeof(ConfirmDialogViewModel))
            {
                return _serviceProvider.GetRequiredService<ConfirmDialog>();
            }
            else if (viewModelType == typeof(ErrorDialogViewModel))
            {
                return _serviceProvider.GetRequiredService<ErrorDialog>();
            }
            else
            {
                throw new Exception($"Unknown view model: {viewModelType}.");
            }
        }

    }
}
