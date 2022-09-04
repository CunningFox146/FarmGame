using Cysharp.Threading.Tasks;
using Farm.Animations;
using Farm.Factories;
using System;
using UnityEngine;
using Zenject;

namespace Farm.GrowSystem
{
    [RequireComponent(typeof(AnimationSystem))]
    public class Corn : Plant
    {
        private static int GrowAnimation = Animator.StringToHash("Grow");
        private static int IdleAnimation = Animator.StringToHash("Idle");

        private AnimationSystem _animation;

        [Inject]
        private void Constructor(TestPickable.Factory factory)
        {
            _collectable.ProductFactory = factory;
        }

        private void Awake()
        {
            _animation = GetComponent<AnimationSystem>();
        }

        protected override async void UpdateStage(int stage)
        {
            _animation.Play(GrowAnimation);
            await UniTask.Delay(TimeSpan.FromSeconds(37/(double)60));
            base.UpdateStage(stage);
        }

        public class Factory : PlaceholderFactory<Corn> { }
    }
}
