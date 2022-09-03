using Farm.Interactable;
using UnityEngine;

namespace Farm.WalkSystem
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        private Source _source;

        [field: SerializeField] public InteractionSettings InteractionSettings { get; private set; }
        public IInteractionLogic InteractionSource => _source;


        private void Awake()
        {
            _source = new(this, InteractionSettings);
        }

        public class Source : InteractionLogicComponent<Walkable>
        {
            public Source(Walkable target, InteractionSettings settings) : base(target, settings) { }

            // InteractionSystem automatically walks to interaction point, so we don't need to do anything here
            public override bool Interact(GameObject doer, InteractionData info) => true;

            public override bool IsValid(GameObject doer)
            {
                return doer.GetComponent<Movement>() is not null;
            }
        }
    }
}
