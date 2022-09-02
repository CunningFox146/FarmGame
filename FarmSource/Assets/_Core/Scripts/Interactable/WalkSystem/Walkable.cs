using UnityEngine;

namespace Farm.Interactable.WalkSystem
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        [SerializeField] public WalkSource _source;

        public InteractionSource GetSource() => _source;
    }
}
