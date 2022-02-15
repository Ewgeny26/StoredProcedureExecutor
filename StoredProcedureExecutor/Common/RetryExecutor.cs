using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Common
{
    public class RetryExecutor
    {
        private int _retryCount;
        private int _retryDelay;
        public RetryExecutor(int retryCount, int retryDelayInSeconds)
        {
            _retryCount = retryCount;
            _retryDelay = retryDelayInSeconds;
        }

        public async Task RetryAsync(Func<Task> func)
        {
            var retries = 0;
            while (true)
            {
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
                    else
                    {
                        await Task.Delay(_retryDelay * 1000);
                    }
                }
            }
        }
    }
}
