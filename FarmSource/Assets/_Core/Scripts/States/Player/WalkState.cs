using Farm.Animations;

namespace Farm.States.Player
{
    public sealed class WalkState : PlayerState
    {
        public WalkState(AnimationSystem animator) : base(animator)
        {
            OnEnter = OnEnterState;
        }

        private void OnEnterState()
        {
            Animation.Play(PlayerAnimations.Run);
        }
    }
}
