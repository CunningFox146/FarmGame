using Farm.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Farm.UI.InventoryUI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Button _button;

        internal void SetItem(ItemInfo info)
        {
            _itemIcon.sprite = info?.Icon;
        }
    }
}
