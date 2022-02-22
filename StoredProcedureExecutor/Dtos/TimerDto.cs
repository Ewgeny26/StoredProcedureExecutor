using System;
using System.Diagnostics;

namespace StoredProcedureExecutor.Dtos
{
    public class TimerDto
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public DateTime StartAt { get; private set; }
        public TimeSpan TimeElapsed => _stopwatch.Elapsed;

        public void Start()
        {
            StartAt = DateTime.UtcNow;
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}