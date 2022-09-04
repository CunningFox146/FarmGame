using Farm.InventorySystem;
using UnityEngine;

namespace Farm.Interactable
{
    public struct InteractionData
    {
        public Vector3 Point { get; set; }
        public InventoryItem ActiveItem { get; set; }
    }
}
