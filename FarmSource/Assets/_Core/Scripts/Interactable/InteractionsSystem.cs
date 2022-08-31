using Cysharp.Threading.Tasks;
using Farm.Systems;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Farm.Interactable
{
    [RequireComponent(typeof(Movement))]
    public class InteractionsSystem : MonoBehaviour
    {
        private Movement _movement;
        private IInteractable _target;
        private CancellationTokenSource _targetCt;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        private void OnDisable()
        {
            _targetCt?.Cancel();
            _targetCt = null;
        }

        public void Interact(Transform target, InteractionInfo info)
        {
            var interactions = CollectInteractions(target);
            if (interactions.Count == 0) return;

            var interaction = interactions[0];
            if (interaction is not null)
            {
                _targetCt?.Cancel();
                _targetCt = new();

                StartInteraction(info, interaction, _targetCt.Token);
            }
        }

        private async void StartInteraction(InteractionInfo info, IInteractable interaction, CancellationToken cancellationToken)
        {
            _target = interaction;

            _movement.SetDestination(info.Point, interaction.Distance);

            var ct = this.GetCancellationTokenOnDestroy();
            while (_movement.IsMoving)
            {
                if (cancellationToken.IsCancellationRequested || ct.IsCancellationRequested) break;
                await UniTask.Yield();
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                _target.Interact(gameObject, info);
            }
        }

        private List<IInteractable> CollectInteractions(Transform target)
        {
            var interactions = new List<IInteractable>();

            foreach (IInteractable interactable in GetInteractables(target))
            {
                if (!interactable.IsValid(gameObject)) continue;
                interactions.Add(interactable);
            }

            interactions.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            return interactions;
        }

        private IEnumerable<IInteractable> GetInteractables(Transform target)
        {
            return target.GetComponents<IInteractable>();
        }
    }
}
