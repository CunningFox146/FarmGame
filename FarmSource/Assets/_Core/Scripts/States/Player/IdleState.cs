using Farm.Animations;

namespace Farm.States.Player
{
    public sealed class IdleState : PlayerState
    {
        public IdleState(AnimationSystem animator) : base(animator)
        {
            OnEnter = OnEnterState;
        }

        private void OnEnterState()
        {
            Animation.Play(PlayerAnimations.Idle);
        }
    }
}
