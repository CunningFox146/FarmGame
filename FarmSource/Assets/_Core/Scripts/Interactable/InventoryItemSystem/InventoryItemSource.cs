using Farm.InventorySystem;
using UnityEngine;

namespace Farm.Interactable.InventoryItemSystem
{
    [CreateAssetMenu(menuName = "Interaction Source / InventoryItem")]
    public class InventoryItemSource : InteractionSource
    {
        private InventoryItem _target;

        public void Init(InventoryItem target)
        {
            _target = target;
        }

        public override bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Inventory>() is not null && _target.IsInteractable;
        }

        public override bool Interact(GameObject doer, InteractionData info)
        {
            var inventory = doer.GetComponent<Inventory>();
            inventory.Put(_target);
            return true;
        }
    }
}
