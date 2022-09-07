using System;
using System.Linq;
using UnityEngine;

namespace Farm.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public event Action<InventoryItem, int> ItemAdded;
        public event Action<InventoryItem, int> ItemRemoved;
        public event Action<InventoryItem, int> ActiveItemChanged;
        public event Action<int> Resized;

        [SerializeField] private int _maxSize;

        public InventoryItem[] Items { get; private set; }
        public InventoryItem ActiveItem { get; private set; }
        public int Size => Items.Count(e => e is not null);
        public int MaxSize
        {
            get => _maxSize;
            set
            {
                if (_maxSize == value) return;
                _maxSize = value;
                Resize();
            }
        }
        public bool IsFull => Size >= MaxSize;

        private void Awake()
        {
            Resize();
        }

        public void Put(InventoryItem item)
        {
            if (IsFull)
            {
                item.OnDropped(this);
                return;
            }

            int slot = GetFreeSlot();
            Items[slot] = item;
            item.OnPutInInventory(this);
            ItemAdded?.Invoke(item, slot);
        }

        public void Drop(int slot)
        {
            var item = Items[slot];
            RemoveItem(item, slot);
        }

        public void Drop(InventoryItem item)
        {
            int slot = GetItemSlot(item);
            RemoveItem(item, slot);
        }

        public void SetActiveItem(InventoryItem item)
        {
            int slot = GetItemSlot(item);
            ActivateItem(item, slot);
        }

        public void SetActiveItem(int slot)
        {
            var item = Items[slot];
            ActivateItem(item, slot);
        }

        private void ActivateItem(InventoryItem item, int slot)
        {
            if (item is null)
            {
                ClearActiveItem();
                return;
            }

            ActiveItem = item;
            ActiveItemChanged?.Invoke(item, slot);
        }

        public void ClearActiveItem()
        {
            ActiveItem = null;
            ActiveItemChanged?.Invoke(null, -1);
        }

        private void RemoveItem(InventoryItem item, int slot)
        {
            if (item is null) return;
            Items[slot] = null;
            item.OnDropped(this);
            ItemRemoved?.Invoke(item, slot);
        }

        private int GetFreeSlot()
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (Items[i] is null) return i;
            }
            return 0;
        }

        private int GetItemSlot(InventoryItem item)
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (Items[i] == item) return i;
            }
            return 0;
        }

        private void Resize()
        {
            Items = new InventoryItem[MaxSize];
            Resized?.Invoke(MaxSize);
        }
    }
}
