using Farm.InventorySystem;
using Farm.Player;
using UnityEngine;

namespace Farm.Interactable.CollectSystem
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableInfo _info;
        private Source _source;

        [field: SerializeField] public float WorkTime { get; private set; }
        [field: SerializeField] public InventoryItem ProductPrefab { get; private set; }
        [field: SerializeField] public bool IsCollectable { get; private set; }

        public InteractionSource InteractionSource => _source;

        private void Awake()
        {
            _source = new(this, _info);
        }

        public class Source : InteractionSourceComponent<Collectable>
        {
            public Source(Collectable target, InteractableInfo info) : base(target, info) { }

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
