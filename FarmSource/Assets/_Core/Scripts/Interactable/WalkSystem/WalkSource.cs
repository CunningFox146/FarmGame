using Farm.Systems;
using UnityEngine;

namespace Farm.Interactable.WalkSystem
{
    [CreateAssetMenu(menuName = "Interaction Source / Walk")]
    public class WalkSource : InteractionSource
    {
        // InteractionSystem automatically walks to interaction point, so we don't need to do anything here
        public override bool Interact(GameObject doer, InteractionData info) => true;

        public override bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Movement>() is not null;
        }
    }
}
