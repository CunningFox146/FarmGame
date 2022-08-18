using Farm.States;
using Farm.Systems;
using System;
using UnityEngine;

namespace Farm.Player
{
    [RequireComponent(typeof(Movement))]
    public sealed class PlayerStates : StateSystem
    {
        private Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        private void Start()
        {
            GoToState(new States.Player.IdleState());
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
            GoToState(new States.Player.WalkState());
        }

        private void OnMovementStopHandler()
        {
            GoToState(new States.Player.IdleState());
        }
    }
}
