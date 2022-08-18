using UnityEngine;

namespace Farm.States.Player
{
    public sealed class IdleState : State
    {
        public IdleState()
        {
            OnEnter = OnEnterState;
        }

        private void OnEnterState()
        {
            Debug.Log("Entered idle");
        }
    }
}
