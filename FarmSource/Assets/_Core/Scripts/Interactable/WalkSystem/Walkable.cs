using Farm.Systems;
using UnityEngine;

namespace Farm.Interactable.WalkSystem
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        [SerializeField] public InteractableInfo _info;
        private Source _source;

        public InteractionSource InteractionSource => _source;

        private void Awake()
        {
            _source = new(this, _info);
        }

        public class Source : InteractionSourceComponent<Walkable>
        {
            public Source(Walkable target, InteractableInfo info) : base(target, info) { }

            // InteractionSystem automatically walks to interaction point, so we don't need to do anything here
            public override bool Interact(GameObject doer, InteractionData info) => true;

            public override bool IsValid(GameObject doer)
            {
                return doer.GetComponent<Movement>() is not null;
            }
        }
    }
}
