using Cysharp.Threading.Tasks;
using Farm.Animations;
using System;
using UnityEngine;

namespace Farm.Plants
{
    [RequireComponent(typeof(AnimationSystem))]
    public class AnimatedPlant : Plant
    {
        [SerializeField] protected double _animationLength = 37 / (double)60;
        private AnimationSystem _animation;

        protected virtual void Awake()
        {
            _animation = GetComponent<AnimationSystem>();
        }

        protected override async void UpdateStage(int stage)
        {
            _animation.Play(PlantsAnimations.Grow);
            await UniTask.Delay(TimeSpan.FromSeconds(_animationLength));
            base.UpdateStage(stage);
        }
    }
}
