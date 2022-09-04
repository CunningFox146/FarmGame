﻿using Farm.CollectSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.GrowSystem
{
    public class Plant : Growable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] protected Collectable _collectable;
        [SerializeField] private List<Sprite> _sprites;

        private void Awake()
        {
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

        protected virtual void UpdateStage(int stage)
        {
            _spriteRenderer.sprite = _sprites[stage];
            if (IsFull)
            {
                _collectable.Regrow();
            }
        }
    }
}
