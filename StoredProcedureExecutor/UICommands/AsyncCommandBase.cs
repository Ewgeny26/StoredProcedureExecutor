using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoredProcedureExecutor.UICommands
{
    public abstract class AsyncCommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract bool CanExecute(object? parameter);

        public async void Execute(object? parameter)
        {
            await ExecuteAsync(parameter);
            CommandManager.InvalidateRequerySuggested();
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
