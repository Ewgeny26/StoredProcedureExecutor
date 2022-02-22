using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Common
{
    public class RetryExecutor
    {
        private const int NumberOfMillisecondsInSeconds = 1000;

        private readonly int _retryCount;
        private readonly int _retryDelay;

        public RetryExecutor(int retryCount, int retryDelayInSeconds)
        {
            _retryCount = retryCount;
            _retryDelay = retryDelayInSeconds;
        }

        public async Task RetryAsync(Func<Task> func)
        {
            var retries = 0;
            while (true)
                try
                {
                    retries++;
                    await func();
                    break;
                }
                catch
                {
                    if (retries == _retryCount)
                    {
                        throw;
                    }

                    await Task.Delay(_retryDelay * NumberOfMillisecondsInSeconds);
                }
        }
    }
}