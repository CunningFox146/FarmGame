using Farm.Player;
using Farm.States;
using UnityEngine;

namespace Farm.Interactable
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _workTime;

        int IInteractable.Priority { get; set; } = 1;
        float IInteractable.Distance { get; set; } = 2f;

        public bool Interact(GameObject doer, InteractionInfo info)
        {
            var stateSystem = doer.GetComponent<PlayerStates>();

            stateSystem.StartWorking();

            var state = stateSystem.CurrentState;
            state.TimeoutTime = _workTime;
            state.OnTimeout = () =>
            {
                stateSystem.StartIdle();
            };

            return true;
        }

        public bool IsValid(GameObject doer)
        {
            return true;
        }
    }
}
