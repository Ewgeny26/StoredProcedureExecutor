using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Configurations;
using System.Collections.ObjectModel;
using StoredProcedureExecutor.Infrastructure.Contracts;

namespace StoredProcedureExecutor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ConfigurationFactory _configurationFactory;
        private readonly INavigationService _navigationService;
        private string _currentEnvironment;

        public MainWindowViewModel(
            ConfigurationFactory configurationFactory,
            INavigationService navigationService)
        {
            _configurationFactory = configurationFactory;
            Environments = _configurationFactory.AllowedEnvironments.ToObservableCollection();
            _currentEnvironment = _configurationFactory.CurrentEnvironment;
            _navigationService = navigationService;
        }

        public ObservableCollection<string> Environments { get; set; }

        public string CurrentEnvironment
        {
            get => _currentEnvironment;
            set => SetEnvironment(value);
        }

        private async void SetEnvironment(string env)
        {
            _configurationFactory.SetEnvironment(env);
            SetProperty(ref _currentEnvironment, env);
            await _navigationService.NavigateTo<ProceduresViewModel>();
        }
    }
}