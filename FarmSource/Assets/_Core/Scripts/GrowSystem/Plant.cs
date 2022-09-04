using Farm.Interactable.CollectSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.GrowSystem
{
    [RequireComponent(typeof(Collectable))]
    public class Plant : Growable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private List<Sprite> _sprites;
        private Collectable _collectable;

        private void Awake()
        {
            _collectable = GetComponent<Collectable>();

            UpdateStage(CurrentStage);
        }

        private void OnEnable()
        {
            StageChanged += OnStageChangedHandler;
            _collectable.Picked += OnPickedHandler;
        }

        private void OnDisable()
        {
            StageChanged -= OnStageChangedHandler;
            _collectable.Picked -= OnPickedHandler;
        }

        private void OnPickedHandler()
        {
            // TODO: Destroy? Reset stage?
        }

        private void OnStageChangedHandler(int stage)
        {
            UpdateStage(stage);
        }

        private void UpdateStage(int stage)
        {
            _spriteRenderer.sprite = _sprites[stage];
            if (IsFull)
            {
                _collectable.Regrow();
            }
        }
    }
}
