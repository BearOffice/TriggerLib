using System;

namespace TriggerLib
{
    public class TriggerSource
    {
        /// <summary>
        /// Get the Trigger.
        /// </summary>
        public Trigger Trigger { get; private set; }
        private readonly int _interval;
        private readonly Action _finalAction;

        /// <summary>
        /// Create a TriggerSource.
        /// </summary>
        /// <param name="interval">The interval of trigger.</param>
        /// <param name="finalAction">The action to be executed.</param>
        /// <param name="pullImmed">The trigger will be pulled immediately if true. Default is true.</param>
        public TriggerSource(int interval, Action finalAction, bool pullImmed = true)
        {
            _interval = interval;
            _finalAction = finalAction;
            ResetTrigger(pullImmed);
        }

        /// <summary>
        /// Reset the trigger. The previous trigger will be cancelled.
        /// </summary>
        /// <param name="pullImmed">The trigger will be pulled immediately if true. Default is true.</param>
        public void ResetTrigger(bool pullImmed = true)
        {
            Trigger?.Dispose();
            Trigger = new Trigger(_interval);

            Trigger.Result += result =>
            {
                if (result) _finalAction?.Invoke();
            };

            if (pullImmed) Pull();
        }

        /// <summary>
        /// Pull the trigger.
        /// </summary>
        public void Pull()
            => Trigger.Pull();

        /// <summary>
        /// Cancel the current trigger.
        /// </summary>
        public void Cancel()
            => ResetTrigger(false);
    }
}
