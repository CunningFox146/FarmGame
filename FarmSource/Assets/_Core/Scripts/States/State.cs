using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Farm.States
{
    public abstract class State
    {
        public float TimeoutTime { get; set; } = -1f;
        public bool IsDone { get; private set; }
        public bool IsTimedOut { get; private set; }

        public List<TimeEvent> TimeEvents { get; protected set; }

        public Action OnUpdate { get; set; }
        public Action OnEnter { get; set; }
        public Action OnTimeout { get; set; }
        public Action OnExit { get; set; }

        public static async void WaitForExit(State state)
        {
            while (!state.IsDone)
            {
                await UniTask.Yield();
            }
        }

        public static async void WaitForTimeout(State state)
        {
            while (!state.IsTimedOut && !state.IsDone)
            {
                await UniTask.Yield();
            }
        }

        public virtual void Exit()
        {
            IsDone = true;
            if (OnExit is not null)
            {
                OnExit();
            }
        }

        public virtual void Timeout()
        {
            IsTimedOut = true;
            if (OnTimeout is not null)
            {
                OnTimeout();
            }
        }
    }
}
