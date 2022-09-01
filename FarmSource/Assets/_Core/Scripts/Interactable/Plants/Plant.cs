using Farm.Animations;
using Farm.Interactable.GrowSystem;
using UnityEngine;

namespace Farm.Interactable.Plants
{
    public class Plant : Growable
    {
        [SerializeField] private AnimationSystem _animation;

        private void OnEnable()
        {
            StageChanged += OnStageChangedHandler;
        }

        private void OnDisable()
        {
            StageChanged -= OnStageChangedHandler;
        }

        private void OnStageChangedHandler(int stage)
        {
            
        }
    }
}
