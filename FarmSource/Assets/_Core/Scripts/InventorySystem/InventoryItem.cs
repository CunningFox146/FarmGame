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

        [SerializeField] private InteractableInfo _info;
        private Source _source;

        [field: SerializeField] public ItemInfo Info { get; protected set; }
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

        public InteractionSource InteractionSource => _source;

        private void Awake()
        {
            InitSource();
        }

        private void InitSource()
        {
            _source = new(this, _info);
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

        public class Source : InteractionSourceComponent<InventoryItem>
        {
            public Source(InventoryItem target, InteractableInfo info) : base(target, info) { }

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