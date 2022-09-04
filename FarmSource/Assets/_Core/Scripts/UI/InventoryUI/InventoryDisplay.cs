using Farm.InventorySystem;
using System;
using UnityEngine;

namespace Farm.UI.InventoryUI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventorySlot _slotPrefab;

        private int _slotsCount;
        private int _activeItemSlot;
        private InventorySlot[] _slots;
        private ItemCollectVisualizer itemCollectVisualizer;

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

        [Zenject.Inject]
        private void Constructor(Camera gameplayCamera, ViewSystem viewSystem)
        {
            itemCollectVisualizer = new(gameplayCamera, viewSystem.ViewsCanvas.transform as RectTransform);
        }

        private void Awake()
        {
            SlotsCount = _inventory.MaxSize;
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
            SyncItems();
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
            _inventory.ActiveItemChanged += OnActiveItemChangedHandler;
        }

        private void UnregisterEventHandlers()
        {
            _inventory.ItemAdded -= OnItemAddedHandler;
            _inventory.ItemRemoved -= OnItemRemovedHandler;
            _inventory.ActiveItemChanged -= OnActiveItemChangedHandler;
        }

        public void SetActiveItem(int slot)
        {
            _inventory.SetActiveItem(slot);
            for (int i = 0; i < SlotsCount; i++)
            {
                _slots[i].IsActiveItem = i == slot;
            }
        }

        public void ClearActiveItem()
        {
            _inventory.ClearActiveItem();
            for (int i = 0; i < SlotsCount; i++)
            {
                _slots[i].IsActiveItem = false;
            }
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
            slot.Click += OnSlotClickHandler;
            _slots[i] = slot;
        }

        private void UnregisterSlot(InventorySlot slot)
        {
            Destroy(slot.gameObject);
            slot.Hold -= OnSlotHoldHandler;
            slot.Click -= OnSlotClickHandler;
        }

        private void SyncItems()
        {
            var activeItem = _inventory.ActiveItem;
            for (int i = 0; i < _slotsCount; i++)
            {
                var item = _inventory.Items?[i];
                _slots[i].SetItem(item?.Info);
                _slots[i].IsActiveItem = activeItem is not null && activeItem == item;
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

        private void OnItemAddedHandler(InventoryItem item, int slot)
        {
            _slots[slot].SetItem(item.Info);
            itemCollectVisualizer.Visualize(item, _slots[slot]);
        }

        private void OnItemRemovedHandler(InventoryItem item, int slot)
        {
            _slots[slot].SetItem(null);
        }

        private void OnActiveItemChangedHandler(InventoryItem item, int slot)
        {
            _activeItemSlot = slot;
            for (int i = 0; i < _slotsCount; i++)
            {
                _slots[i].IsActiveItem = i == slot;
            }
        }

        private void OnSlotHoldHandler(int slot)
        {
            _inventory.Drop(slot);
        }

        private void OnSlotClickHandler(int slot)
        {
            if (_activeItemSlot == slot)
            {
                _inventory.ClearActiveItem();
            }
            else
            {
                _activeItemSlot = slot;
                _inventory.SetActiveItem(slot);
            }
        }
    }
}
