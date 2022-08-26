using System.Linq;
using UnityEngine;

namespace Farm.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _maxSize;
        public InventoryItem[] Items { get; private set; }

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
            if (IsFull) return;

            uint slot = GetFreeSlot();
            Items[slot] = item;
            item.OnPutInInventory(this);
        }

        public void Drop(int index)
        {
            var item = Items[index];
            Items[index] = null;
            item.OnDropped(this);
        }

        public void Drop(InventoryItem item)
        {
            uint slot = GetItemSlot(item);
            Items[slot] = null;
            item.OnDropped(this);
        }

        private uint GetFreeSlot()
        {
            for (uint i = 0; i < MaxSize; i++)
            {
                if (Items[i] is null) return i;
            }
            return 0;
        }

        private uint GetItemSlot(InventoryItem item)
        {
            for (uint i = 0; i < MaxSize; i++)
            {
                if (Items[i] == item) return i;
            }
            return 0;
        }

        private void Resize()
        {
            Items = new InventoryItem[MaxSize];
        }

    }
}
