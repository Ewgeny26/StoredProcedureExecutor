using System.Windows.Controls;

namespace StoredProcedureExecutor.Infrastructure.Contracts
{
    public interface IWindowNavigation
    {
        public Grid Main { get; }
        public ProgressBar Loader { get; }
    }
}