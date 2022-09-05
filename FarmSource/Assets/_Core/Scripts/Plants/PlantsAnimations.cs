using UnityEngine;

namespace Farm.Plants
{
    public static class PlantsAnimations
    {
        public static int Grow = Animator.StringToHash(nameof(Grow));
        public static int Idle = Animator.StringToHash(nameof(Idle));
    }
}
