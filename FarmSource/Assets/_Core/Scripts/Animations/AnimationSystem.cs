using UnityEngine;

namespace Farm.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimationSystem : MonoBehaviour
    {
        private Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play(int name, float transitionTime = 0f, int layer = 0)
        {
            _animator.CrossFade(name, transitionTime, layer);
        }
    }
}