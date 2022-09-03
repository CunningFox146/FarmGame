using UnityEngine;

namespace Farm.Interactable
{
    public abstract class InteractionLogicComponent<T> : IInteractionLogic where T : Component
    {
        public T Target { get; protected set; }
        public InteractionSettings Settings { get; private set; }
        public int Priority { get; private set; }
        public float Distance { get; private set; }

        public InteractionLogicComponent(T target, InteractionSettings settings)
        {
            Target = target;
            Settings = settings;

            Priority = Settings.Priority;
            Distance = Settings.Distance;
        }

        public abstract bool IsValid(GameObject doer);

        public abstract bool Interact(GameObject doer, InteractionData info);
    }
}
