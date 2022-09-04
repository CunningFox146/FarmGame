using Farm.InventorySystem;
using UnityEngine;
using Zenject;

namespace Farm.Factories
{
    public class TestPickable : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<TestPickable>, IInventoryItemFactory
        {
            public InventoryItem CreateInventoryItem() => Create().GetComponent<InventoryItem>();
        }
    }
}
