using Farm.Interactable;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.World
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        int IInteractable.Priority { get; set; }
        float IInteractable.Distance { get; set; } = 0f;

        // InteractionSystem automatically walk to interaction point, so we don't need to do anything here
        public bool Interact(GameObject doer, InteractionInfo info) => true

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<NavMeshAgent>() is not null;
        }
    }
}
