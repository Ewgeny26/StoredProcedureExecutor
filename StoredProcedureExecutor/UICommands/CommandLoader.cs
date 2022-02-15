using StoredProcedureExecutor.Infrastructure;
using System.Windows;

namespace StoredProcedureExecutor.UICommands
{
    public class CommandLoader : NotifyPropertyChangedBase
    {
        public Visibility Loader { get; private set; } = Visibility.Hidden;
        public bool BtnEnable { get; private set; } = true;

        public void Start()
        {
            Loader = Visibility.Visible;
            BtnEnable = false;
            OnPropertyChanged(nameof(Loader));
            OnPropertyChanged(nameof(BtnEnable));
        }

        public void Stop()
        {
            Loader = Visibility.Hidden;
            BtnEnable = true;
            OnPropertyChanged(nameof(Loader));
            OnPropertyChanged(nameof(BtnEnable));
        }
    }
}
