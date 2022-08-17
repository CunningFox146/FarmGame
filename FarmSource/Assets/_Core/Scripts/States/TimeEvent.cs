using System;

namespace Farm.States
{
    public class TimeEvent
    {
        public float Delay { get; set; }
        public Action<StateSystem> Callback { get; private set; }

        public TimeEvent(float delay, Action<StateSystem> callback)
        {
            Delay = delay;
            Callback = callback;
        }
    }
}
