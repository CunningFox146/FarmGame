using Farm.Animations;

namespace Farm.States.Player
{
    public class PlayerState : State
    {
        public AnimationSystem Animation { get; private set; }

        public PlayerState(AnimationSystem animation)
        {
            Animation = animation;
        }
    }
}
