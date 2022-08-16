using System.Collections.Generic;

namespace Farm.States
{
    public abstract class State
    {
        public float Timeout { get; set; } = -1f;

        public List<TimeEvent> TimeEvents { get; protected set; }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
        public abstract void OnTimeout();
    }
}
