using Cysharp.Threading.Tasks;
using Farm.WalkSystem;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Farm.Interactable
{
    [RequireComponent(typeof(Movement))]
    public class InteractionsSystem : MonoBehaviour
    {
        private Movement _movement;
        private IInteractionLogic _target;
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

        public void Interact(Transform target, InteractionData info)
        {
            var interactions = CollectInteractions(target, info);
            if (interactions.Count == 0) return;

            var interaction = interactions[0];
            if (interaction is not null)
            {
                _targetCt?.Cancel();
                _targetCt = new();

                StartInteraction(info, interaction, _targetCt.Token);
            }
        }

        private async void StartInteraction(InteractionData info, IInteractionLogic interaction, CancellationToken cancellationToken)
        {
            _target = interaction;

            _movement.SetDestination(info.Point, _target.Distance);

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

        private List<IInteractionLogic> CollectInteractions(Transform target, InteractionData info)
        {
            var interactions = new List<IInteractionLogic>();

            foreach (IInteractable interactable in GetInteractables(target))
            {
                var source = interactable.InteractionSource;
                if (!source.IsValid(gameObject, info)) continue;
                interactions.Add(source);
            }

            interactions.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            return interactions;
        }

        private IEnumerable<IInteractable> GetInteractables(Transform target)
        {
            return target.root.GetComponentsInChildren<IInteractable>();
        }
    }
}
