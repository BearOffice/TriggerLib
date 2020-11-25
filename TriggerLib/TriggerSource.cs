using System;

namespace TriggerLib
{
    public class TriggerSource
    {
        public Trigger Trigger { get; private set; }
        private readonly int _interval;
        private readonly Action _finalAction;

        public TriggerSource(int interval, Action finalAction, bool pullImmed = true)
        {
            _interval = interval;
            _finalAction = finalAction;
            CreateNewTrigger(pullImmed);
        }

        public void CreateNewTrigger(bool pullImmed = true)
        {
            Trigger?.Dispose();
            Trigger = new Trigger(_interval);

            Trigger.Result += result =>
            {
                if (result) _finalAction?.Invoke();
            };

            if (pullImmed) Pull();
        }

        public void Pull()
            => Trigger.Pull();

        public void Vanish()
            => CreateNewTrigger(false);
    }
}
