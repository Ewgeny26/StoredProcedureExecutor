using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.UICommands
{
    public class AsyncCompositeCommand : AsyncCommandBase
    {
        protected readonly IEnumerable<AsyncCommandBase> _commands;
        protected readonly CommandLoader _loader;
        public AsyncCompositeCommand(CommandLoader loader, IEnumerable<AsyncCommandBase> commands)
        {
            _commands = commands;
            _loader = loader;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            _loader.Start();
            try
            {
                foreach (var command in _commands)
                {
                    await command.ExecuteAsync(parameter);
                }
            }
            finally
            {
                _loader.Stop();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _commands.Select(c => c.CanExecute(parameter)).All(result => result == true);
        }
    }
}
