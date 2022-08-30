using Farm.InventorySystem;
using Farm.UI.Elements;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Farm.UI.InventoryUI
{
    public class InventorySlot : MonoBehaviour
    {
        public event Action<int> Hold;

        [SerializeField] private Image _itemIcon;
        [SerializeField] private BasicButton _button;

        private InventoryDisplay _inventory;

        public Image ItemIcon => _itemIcon;
        public int Index { get; private set; }

        private void OnEnable()
        {
            _button.OnHold += OnHoldHandler;
        }

        private void OnDisable()
        {
            _button.OnHold -= OnHoldHandler;
        }

        public void Init(InventoryDisplay inventory, int idx)
        {
            _inventory = inventory;
            Index = idx;
            name = $"Slot #{Index}";
        }

        public void SetItem(ItemInfo info)
        {
            _itemIcon.gameObject.SetActive(info is not null && info.Icon is not null);
            _itemIcon.sprite = info?.Icon;
        }

        private void OnHoldHandler()
        {
            Hold?.Invoke(Index);
        }
    }
}
