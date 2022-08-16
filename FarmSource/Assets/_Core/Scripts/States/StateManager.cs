using System;
using UnityEngine;

namespace Farm.States
{
    public class StateManager : MonoBehaviour
    {
        public event Action<State> StateExit;
        public event Action<State> StateEnter;

        private State _currentState;

        public State CurrentState { get => _currentState; private set => _currentState = value; }

        private void Update()
        {
            if (CurrentState is not null)
            {
                UpdateCurrentState();
            }
        }

        public void GoToState(State state)
        {
            CurrentState?.OnExit();
            StateExit?.Invoke(CurrentState);

            CurrentState = state;
            CurrentState?.OnEnter();
            StateEnter?.Invoke(CurrentState);
        }

        private void UpdateCurrentState()
        {
            UpdateTimeline();
            UpdateTimeout();
            CurrentState.OnUpdate();
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
                CurrentState.OnTimeout();
            }
        }
    }
}
