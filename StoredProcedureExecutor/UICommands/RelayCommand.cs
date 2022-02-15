using System;
using System.Windows.Input;

namespace StoredProcedureExecutor.UICommands
{
    public class RelayCommand : ICommand
    {
        readonly Action _execute;
        readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return _canExecute == null ? true : _canExecute.Invoke();
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute()
        {
            _execute();
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecute();
        }

        public void Execute(object? parameter)
        {
            Execute();
        }
    }
}
