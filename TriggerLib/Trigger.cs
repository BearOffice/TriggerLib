using System;
using System.Threading;
using System.Threading.Tasks;

namespace TriggerLib
{
    public class Trigger : IDisposable
    {
        /// <summary>
        /// true if trigger is counting down; otherwise, false.
        /// </summary>
        public bool IsCountingDown { get; private set; } = false;
        /// <summary>
        /// true if trigger is pulled; otherwise, false.
        /// </summary>
        public bool IsPulled { get; private set; } = false;
        /// <summary>
        /// Publish event while the trigger finished or be interrupted.
        /// </summary>
        public event Action<bool> Result;
        private readonly CancellationTokenSource _source = new CancellationTokenSource();
        private readonly int _interval;
        private bool _disposed = false;

        /// <summary>
        /// Create a Trigger.
        /// </summary>
        /// <param name="interval"></param>
        public Trigger(int interval)
            => _interval = interval;

        private async Task CountDown()
        {
            await Task.Delay(_interval, _source.Token)
                .ContinueWith(t =>
                {
                    Result.Invoke(!t.IsCanceled);
                });

            IsCountingDown = false;
        }

        /// <summary>
        /// Pull the trigger.
        /// </summary>
        public void Pull()
        {
            if (IsPulled) return;

            IsCountingDown = true;
            Task.Run(() => CountDown());
            IsPulled = true;
        }

        ~Trigger() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _source.Cancel();

            _disposed = true;
        }
    }
}
