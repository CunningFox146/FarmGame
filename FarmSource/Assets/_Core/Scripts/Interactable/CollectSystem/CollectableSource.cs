using Farm.InventorySystem;
using Farm.Player;
using UnityEngine;

namespace Farm.Interactable.CollectSystem
{
    [CreateAssetMenu(menuName = "Interaction Source / Collectable")]
    public class CollectableSource : InteractionSource
    {
        private Collectable _target;

        public void Init(Collectable target)
        {
            _target = target;
        }

        public override bool Interact(GameObject doer, InteractionData info)
        {
            var stateSystem = doer.GetComponent<PlayerStates>();

            stateSystem.StartWorking();

            var state = stateSystem.CurrentState;
            state.TimeoutTime = _target.WorkTime;
            state.OnTimeout = () =>
            {
                var inventory = doer.GetComponent<Inventory>();
                var product = Instantiate(_target.ProductPrefab);
                inventory.Put(product);

                stateSystem.StartIdle();
            };

            return true;
        }

        public override bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Inventory>() && _target.IsCollectable;
        }
    }
}
