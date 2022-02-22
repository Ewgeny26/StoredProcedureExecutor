using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.UICommands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private readonly CommandLoader? _loader;

        public AsyncRelayCommand(Func<Task> execute, CommandLoader? loader = null, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _loader = loader;
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _loader?.Start();
                await (_execute());
            }
            finally
            {
                _loader?.Stop();
            }
        }
    }
}