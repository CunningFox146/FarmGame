using UnityEngine;

namespace Farm.Interactable
{
    public interface IInteractionLogic
    {
        public int Priority { get; }
        public float Distance { get; }

        public abstract bool IsValid(GameObject doer);
        public abstract bool Interact(GameObject doer, InteractionData info);
    }
}