using Farm.InventorySystem;
using Farm.Player;
using System;
using UnityEngine;

namespace Farm.Interactable.CollectSystem
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        public event Action Regrown; 
        public event Action Picked; 

        private Source _source;

        [field: SerializeField] public InteractionSettings InteractionSettings { get; private set; }
        [field: SerializeField] public float WorkTime { get; private set; }
        [field: SerializeField] public InventoryItem ProductPrefab { get; private set; }
        [field: SerializeField] public bool IsCollectable { get; private set; }

        public IInteractionLogic InteractionSource => _source;

        private void Awake()
        {
            _source = new(this, InteractionSettings);
        }

        public void Regrow()
        {
            IsCollectable = true;
            Regrown?.Invoke();
        }

        public void OnPicked()
        {
            IsCollectable = false;
            Picked?.Invoke();
        }

        public class Source : InteractionLogicComponent<Collectable>
        {
            public Source(Collectable target, InteractionSettings settings) : base(target, settings) { }

            public override bool Interact(GameObject doer, InteractionData info)
            {
                var stateSystem = doer.GetComponent<PlayerStates>();

                stateSystem.StartWorking();

                var state = stateSystem.CurrentState;
                state.TimeoutTime = Target.WorkTime;
                state.OnTimeout = () =>
                {
                    var inventory = doer.GetComponent<Inventory>();
                    var product = Instantiate(Target.ProductPrefab);
                    inventory.Put(product);
                    Target.OnPicked();

                    stateSystem.StartIdle();
                };

                return true;
            }

            public override bool IsValid(GameObject doer)
            {
                return doer.GetComponent<Inventory>() && Target.IsCollectable;
            }
        }
    }
}
