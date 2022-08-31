using Farm.Interactable;
using Farm.Util;
using System;
using UnityEngine;

namespace Farm.InventorySystem
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public ItemInfo Info { get; protected set; }
        [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

        int IInteractable.Priority { get; set; } = 1;
        float IInteractable.Distance { get; set; } = 2f;

        public event Action<Inventory> PutInInventory;
        public event Action<Inventory> Dropped;

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

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Inventory>() is not null && IsInteractable;
        }

        public bool Interact(GameObject doer, InteractionInfo info)
        {
            var inventory = doer.GetComponent<Inventory>();
            inventory.Put(this);
            return true;
        }
    }
}