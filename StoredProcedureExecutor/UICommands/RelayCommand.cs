using System;
using System.Windows.Input;

namespace StoredProcedureExecutor.UICommands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return _canExecute?.Invoke() ?? true;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
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