using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Constracts;
using System.Collections.ObjectModel;

namespace StoredProcedureExecutor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ConfigurationFactory _configurationFactory;
        private readonly IDialogsService _dialogsService;
        private readonly INavigationService _navigationService;
        private string _currentEnviroment;

        public MainWindowViewModel(
            ConfigurationFactory configurationFactory,
            IDialogsService dialogsService,
            INavigationService navigationService)
        {
            _configurationFactory = configurationFactory;
            _dialogsService = dialogsService;
            Enviroments = _configurationFactory.AllowedEnviroments.ToObservableCollection();
            _currentEnviroment = _configurationFactory.CurrentEnviroment;
            _navigationService = navigationService;
        }

        public ObservableCollection<string> Enviroments { get; set; }
        public string CurrentEnviroment
        {
            get => _currentEnviroment;
            set => SetEnviroment(value);
        }

        private async void SetEnviroment(string env)
        {
            //var result = await _dialogsService.Show<ConfirmDialogViewModel, bool>($"Are you sure change enviroment on [{env}]");
            //if (result == true)
            //{
            _configurationFactory.SetEnviroment(env);
            SetProperty(ref _currentEnviroment, env);
            await _navigationService.NavigateTo<ProceduresViewModel>(null, null);
            //}
        }
    }
}
