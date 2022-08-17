using System.Collections.Generic;
using UnityEngine;

namespace Farm.Interactable
{
    public class InteractionsSystem : MonoBehaviour
    {
        public void Interact(GameObject target)
        {
            var interaction = CollectInteractions(target)[0];
            if (interaction is not null)
            {
                interaction.Interact(gameObject);
            }
        }

        private List<IInteractable> CollectInteractions(GameObject target)
        {
            var interactions = new List<IInteractable>();

            foreach (IInteractable interactable in target.GetComponents<IInteractable>())
            {
                if (!interactable.IsValid(gameObject)) continue;
                interactions.Add(interactable);
            }

            interactions.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            return interactions;
        }
    }
}
