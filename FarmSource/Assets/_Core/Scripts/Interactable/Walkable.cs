using Farm.Systems;
using UnityEngine;

namespace Farm.Interactable
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        [field: SerializeField] int IInteractable.Priority { get; set; }
        [field: SerializeField] float IInteractable.Distance { get; set; } = 0.1f;

        // InteractionSystem automatically walks to interaction point, so we don't need to do anything here
        public bool Interact(GameObject doer, InteractionInfo info) => true;

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<Movement>() is not null;
        }
    }
}
