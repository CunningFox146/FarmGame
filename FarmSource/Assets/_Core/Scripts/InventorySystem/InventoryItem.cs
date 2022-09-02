using Farm.Interactable;
using Farm.Interactable.InventoryItemSystem;
using Farm.Util;
using System;
using UnityEngine;

namespace Farm.InventorySystem
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
        public event Action<Inventory> PutInInventory;
        public event Action<Inventory> Dropped;

        [SerializeField] private InventoryItemSource _source;

        [field: SerializeField] public ItemInfo Info { get; protected set; }
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;
        
        public InteractionSource GetSource() => _source;

        private void Awake()
        {
            InitSource();
        }

        private void InitSource()
        {
            _source = Instantiate(_source);
            _source?.Init(this);
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
    }
}