using UnityEngine;

namespace Farm.Interactable
{
    public abstract class InteractionSource
    {
        public int Priority { get; protected set; }
        public float Distance { get; protected set; } = 1f;

        public abstract bool IsValid(GameObject doer);
        public abstract bool Interact(GameObject doer, InteractionData info);
    }
}