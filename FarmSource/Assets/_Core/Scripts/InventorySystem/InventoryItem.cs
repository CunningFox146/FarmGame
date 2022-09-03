using Farm.Interactable;
using Farm.Util;
using System;
using UnityEngine;

namespace Farm.InventorySystem
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
        public event Action<Inventory> PutInInventory;
        public event Action<Inventory> Dropped;

        private Source _source;

        [field: SerializeField] public InteractionSettings InteractionSettings { get; private set; }
        [field: SerializeField] public ItemInfo Info { get; protected set; }
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

        public IInteractionLogic InteractionSource => _source;

        private void Awake()
        {
            InitSource();
        }

        private void InitSource()
        {
            _source = new(this, InteractionSettings);
        }

        public void OnPutInInventory(Inventory inventory)
        {
            PutInInventory?.Invoke(inventory);
            gameObject.SetActive(false);
            transform.SetParent(inventory.transform);
            transform.localPosition = Vector3.zero;
        }

        public void OnDropped(Inventory inventory)
        {
            Dropped?.Invoke(inventory);
            gameObject.SetActive(true);
            transform.SetParent(null);

            if (TryGetComponent(out Rigidbody rigidbody))
            {
                var direction = Vector3.right * 2f * RandomUtil.RandomSign();
                direction.y = 2f;
                rigidbody.AddForce(direction, ForceMode.Impulse);
            }
        }

        public class Source : InteractionLogicComponent<InventoryItem>
        {
            public Source(InventoryItem target, InteractionSettings settings) : base(target, settings) { }

            public override bool IsValid(GameObject doer)
            {
                return doer.GetComponent<Inventory>() is not null && Target.IsInteractable;
            }

            public override bool Interact(GameObject doer, InteractionData info)
            {
                var inventory = doer.GetComponent<Inventory>();
                inventory.Put(Target);
                return true;
            }
        }
    }
}