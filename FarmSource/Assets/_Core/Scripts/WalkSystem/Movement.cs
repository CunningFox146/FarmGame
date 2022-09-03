using System;
using UnityEngine;

namespace Farm.WalkSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public event Action MovementStart;
        public event Action MovementStop;

        private Rigidbody _rigidbody;
        private Vector3 _destination;
        private float _targetDistance;
        private bool _shouldMove;

        [field: SerializeField] public float Speed { get; private set; }
        public bool IsMoving { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetDestination(Vector3 destination, float distance)
        {
            Stop();
            _destination = destination;
            _targetDistance = distance;
            _shouldMove = true;
            IsMoving = true;
            MovementStart?.Invoke();
        }

        private void FixedUpdate()
        {
            bool wasMoving = IsMoving;
            IsMoving = _shouldMove && Vector3.Distance(_destination, transform.position) > _targetDistance;
            if (IsMoving)
            {
                var lookAtTarget = _destination;
                lookAtTarget.y = 0;
                transform.LookAt(lookAtTarget);
                _rigidbody.velocity = (_destination - transform.position).normalized * Speed;
            }
            else if (wasMoving)
            {
                MovementStop?.Invoke();
                Stop();
            }
        }

        public void Stop()
        {
            _shouldMove = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
