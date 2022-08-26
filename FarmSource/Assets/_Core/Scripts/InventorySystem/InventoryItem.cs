using Farm.Interactable;
using System;
using UnityEngine;

namespace Farm.InventorySystem
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
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

            // TODO: Do drop physics?
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