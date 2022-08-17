using UnityEngine;

namespace Farm.Interactable
{
    public interface IInteractable
    {
        public int Priority { get; protected set; }
        public bool IsValid(GameObject doer);
        public bool Interact(GameObject doer);
    }
}
