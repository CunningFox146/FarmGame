using Farm.InventorySystem;
using Farm.Player;
using UnityEngine;

namespace Farm.Interactable.CollectSystem
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _workTime;
        [SerializeField] private InventoryItem _productPrefab;
        [field: SerializeField] public bool IsCollectable { get; private set; }

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
                var inventory = doer.GetComponent<Inventory>();
                var product = Instantiate(_productPrefab);
                inventory.Put(product);

                stateSystem.StartIdle();
            };

            return true;
        }

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Inventory>() && IsCollectable;
        }
    }
}
