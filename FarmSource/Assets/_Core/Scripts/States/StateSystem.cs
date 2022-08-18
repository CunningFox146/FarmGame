using System;
using UnityEngine;

namespace Farm.States
{
    public class StateSystem : MonoBehaviour
    {
        public event Action<State> StateExit;
        public event Action<State> StateEnter;

        private State _currentState;

        public State CurrentState { get => _currentState; private set => _currentState = value; }

        protected virtual void Update()
        {
            if (CurrentState is not null)
            {
                UpdateCurrentState();
            }
        }

        public void GoToState(State state)
        {
            if (CurrentState?.GetType() == state?.GetType()) return;

            CurrentState?.OnExit();
            StateExit?.Invoke(CurrentState);

            CurrentState = state;
            if (CurrentState.OnEnter is not null)
            {
                CurrentState?.OnEnter();
            }
            StateEnter?.Invoke(CurrentState);
        }

        private void UpdateCurrentState()
        {
            UpdateTimeline();
            UpdateTimeout();
            if (CurrentState.OnUpdate is not null)
            {
                CurrentState.OnUpdate();
            }
        }

        private void UpdateTimeline()
        {
            if (CurrentState.TimeEvents is null) return;

            foreach (TimeEvent timeEvent in CurrentState.TimeEvents)
            {
                if (timeEvent.Delay > 0f)
                {
                    timeEvent.Delay -= Time.deltaTime;
                    if (timeEvent.Delay < 0f)
                    {
                        timeEvent?.Callback(this);
                    }
                }
            }
        }

        private void UpdateTimeout()
        {
            if (CurrentState.Timeout <= -1f) return;
            CurrentState.Timeout -= Time.deltaTime;
            if (CurrentState.Timeout <= 0f)
            {
                CurrentState.Timeout = -1f;
                if (CurrentState.OnTimeout is not null)
                {
                    CurrentState.OnTimeout();
                }
            }
        }
    }
}
