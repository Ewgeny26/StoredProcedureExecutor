namespace StoredProcedureExecutor.Infrastructure
{
    public class SnackbarService : ISnackbarService
    {
        private readonly IWindowSnackbars _windowSnackbars;
        public SnackbarService(IWindowSnackbars windowSnackbars)
        {
            _windowSnackbars = windowSnackbars;
        }
        public void Success(string text)
        {
            _windowSnackbars.Snackbar?.MessageQueue?.Enqueue(text);
        }
    }
}
