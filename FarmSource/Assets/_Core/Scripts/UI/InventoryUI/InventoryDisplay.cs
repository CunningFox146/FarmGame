using Farm.InventorySystem;
using UnityEngine;

namespace Farm.UI.InventoryUI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventorySlot _slotPrefab;

        private int _slotsCount;
        private InventorySlot[] _slots;

        public int SlotsCount
        {
            get => _slotsCount;
            set
            {
                if (_slotsCount == value) return;
                _slotsCount = value;
                RebuildSlots();
            }
        }

        private void Awake()
        {
            SlotsCount = _inventory.MaxSize;
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void OnDestroy()
        {
            ClearSlots();
        }

        private void RegisterEventHandlers()
        {
            _inventory.ItemAdded += OnItemAddedHandler;
            _inventory.ItemRemoved += OnItemRemovedHandler;
        }

        private void UnregisterEventHandlers()
        {
            _inventory.ItemAdded -= OnItemAddedHandler;
            _inventory.ItemRemoved -= OnItemRemovedHandler;
        }

        private void OnItemAddedHandler(InventoryItem item, int slot)
        {
            _slots[slot].SetItem(item.Info);
        }

        private void OnItemRemovedHandler(InventoryItem item, int slot)
        {
            _slots[slot].SetItem(null);
        }

        private void OnSlotHoldHandler(int idx)
        {
            _inventory.Drop(idx);
        }

        private void RebuildSlots()
        {
            ClearSlots();

            _slots = new InventorySlot[_slotsCount];
            for (int i = 0; i < _slotsCount; i++)
            {
                var slot = Instantiate(_slotPrefab, transform);
                RegisterSlot(i, slot);
            }

            SyncItems();
        }

        private void RegisterSlot(int i, InventorySlot slot)
        {
            slot.Init(this, i);
            slot.Hold += OnSlotHoldHandler;
            _slots[i] = slot;
        }
        private void UnregisterSlot(InventorySlot slot)
        {
            Destroy(slot.gameObject);
            slot.Hold -= OnSlotHoldHandler;
        }

        private void SyncItems()
        {
            for (int i = 0; i < _slotsCount; i++)
            {
                var item = _inventory.Items?[i];
                _slots[i].SetItem(item?.Info);
            }
        }

        private void ClearSlots()
        {
            if (_slots is null) return;

            foreach (InventorySlot slot in _slots)
            {
                UnregisterSlot(slot);
            }
        }
    }
}
