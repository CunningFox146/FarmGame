using Farm.Interactable.CollectSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Interactable.GrowSystem
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
            _spriteRenderer.sprite = _sprites[stage];
            if (IsFull)
            {
                _collectable.Regrow();
            }
        }
    }
}
