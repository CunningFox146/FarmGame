using Farm.Animations;
using Farm.States;
using Farm.Systems;
using UnityEngine;

namespace Farm.Player
{
    [RequireComponent(typeof(Movement))]
    public sealed class PlayerStates : StateSystem
    {
        private Movement _movement;
        private AnimationSystem _animation;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _animation = GetComponentInChildren<AnimationSystem>();
        }

        private void Start()
        {
            GoToState(new States.Player.IdleState(_animation));
        }

        private void OnEnable()
        {
            RegisterEventListeners();
        }

        private void OnDisable()
        {
            UnregisterEventListeners();
        }

        private void RegisterEventListeners()
        {
            _movement.MovementStart += OnMovementStartHandler;
            _movement.MovementStop += OnMovementStopHandler;
        }

        private void UnregisterEventListeners()
        {
            _movement.MovementStart -= OnMovementStartHandler;
            _movement.MovementStop -= OnMovementStopHandler;
        }

        private void OnMovementStartHandler()
        {
            GoToState(new States.Player.WalkState(_animation));
        }

        private void OnMovementStopHandler()
        {
            GoToState(new States.Player.IdleState(_animation));
        }
    }
}
