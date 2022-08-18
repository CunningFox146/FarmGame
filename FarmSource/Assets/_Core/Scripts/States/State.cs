using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Farm.States
{
    public abstract class State
    {
        public float Timeout { get; set; } = -1f;
        public bool IsDone { get; private set; }

        public List<TimeEvent> TimeEvents { get; protected set; }

        public Action OnUpdate { get; protected set; }
        public Action OnEnter { get; protected set; }
        public Action OnTimeout { get; protected set; }

        public static async void WaitForExit(State state)
        {
            while (!state.IsDone)
            {
                await UniTask.Yield();
            }
        }

        
        public virtual void OnExit()
        {
            IsDone = true;
        }
    }
}
