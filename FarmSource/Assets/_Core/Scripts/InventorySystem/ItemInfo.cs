using UnityEngine;

namespace Farm.InventorySystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Item Info")]
    public class ItemInfo : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}
