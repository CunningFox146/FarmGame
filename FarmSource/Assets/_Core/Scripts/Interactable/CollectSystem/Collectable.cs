using Farm.InventorySystem;
using UnityEngine;

namespace Farm.Interactable.CollectSystem
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] CollectableSource _source;

        [field: SerializeField] public float WorkTime { get; private set; }
        [field: SerializeField] public InventoryItem ProductPrefab { get; private set; }
        [field: SerializeField] public bool IsCollectable { get; private set; }

        public InteractionSource GetSource() => _source;

        private void Awake()
        {
            _source?.Init(this);
        }
    }
}
