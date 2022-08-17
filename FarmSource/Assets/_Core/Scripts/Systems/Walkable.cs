using Farm.Interactable;
using Farm.Systems;
using UnityEngine;

namespace Farm.World
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        int IInteractable.Priority { get; set; }
        float IInteractable.Distance { get; set; } = 0.1f;

        // InteractionSystem automatically walks to interaction point, so we don't need to do anything here
        public bool Interact(GameObject doer, InteractionInfo info) => true;

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Movement>() is not null;
        }
    }
}
