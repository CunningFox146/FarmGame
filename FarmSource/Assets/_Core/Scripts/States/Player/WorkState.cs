using Farm.Animations;

namespace Farm.States.Player
{
    public class WorkState : PlayerState
    {
        public WorkState(AnimationSystem animation) : base(animation)
        {
            OnEnter = OnEnterState;
        }

        private void OnEnterState()
        {
            Animation.Play(PlayerAnimations.Collect);
        }
    }
}
