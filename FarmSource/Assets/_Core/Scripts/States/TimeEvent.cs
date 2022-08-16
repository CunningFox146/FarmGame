using System;

namespace Farm.States
{
    public class TimeEvent
    {
        public float Delay { get; set; }
        public Action<StateManager> Callback { get; private set; }

        public TimeEvent(float delay, Action<StateManager> callback)
        {
            Delay = delay;
            Callback = callback;
        }
    }
}
