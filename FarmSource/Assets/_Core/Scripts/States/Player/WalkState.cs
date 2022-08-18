using UnityEngine;

namespace Farm.States.Player
{
    public sealed class WalkState : State
    {
        public WalkState()
        {
            OnEnter = OnEnterState;
        }

        private void OnEnterState()
        {
            Debug.Log("Entered Walk");
        }
    }
}
