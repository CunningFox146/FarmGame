using UnityEngine;

namespace Farm.Interactable
{
    public abstract class InteractionSourceComponent<T> : InteractionSource where T : Component
    {
        public T Target { get; protected set; }

        public InteractionSourceComponent(T target, InteractableInfo info)
        {
            Target = target;
            Priority = info.Priority;
            Distance = info.Distance;
        }
    }
}
