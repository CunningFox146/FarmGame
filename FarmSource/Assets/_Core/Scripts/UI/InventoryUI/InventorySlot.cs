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
        public event Action<int> Click;

        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _bg;
        [SerializeField] private BasicButton _button;

        private InventoryDisplay _inventory;
        private bool _isActiveItem;

        public int Index { get; private set; }
        public bool IsActiveItem
        {
            get => _isActiveItem;
            set
            {
                if (_isActiveItem == value) return;
                _isActiveItem = value;
                OnIsActiveItemChanged();
            }
        }

        public Image ItemIcon => _itemIcon;

        private void OnEnable()
        {
            _button.OnClick += OnClickHandler;
            _button.OnHold += OnHoldHandler;
        }

        private void OnDisable()
        {
            _button.OnClick -= OnClickHandler;
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

        private void OnIsActiveItemChanged()
        {
            _bg.color = IsActiveItem ? Color.red : Color.white;
        }

        private void OnHoldHandler()
        {
            Hold?.Invoke(Index);
        }

        private void OnClickHandler()
        {
            Click?.Invoke(Index);
        }
    }
}
