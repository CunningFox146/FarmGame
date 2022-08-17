using Farm.Interactable;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.World
{
    public class Walkable : MonoBehaviour, IInteractable
    {
        int IInteractable.Priority{ get; set; }

        public bool Interact(GameObject doer, InteractionInfo info)
        {
            doer.GetComponent<NavMeshAgent>().SetDestination(info.Point);
            return true;
        }

        public bool IsValid(GameObject doer)
        {
            return doer.GetComponent<NavMeshAgent>() is not null;
        }
    }
}
