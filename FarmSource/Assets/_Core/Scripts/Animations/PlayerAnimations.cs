using UnityEngine;

namespace Farm.Animations
{
    public static class PlayerAnimations
    {
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Collect = Animator.StringToHash(nameof(Collect));
    }
}
