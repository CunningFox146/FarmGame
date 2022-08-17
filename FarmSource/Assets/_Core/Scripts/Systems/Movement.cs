using System.Threading;
using UnityEngine;

namespace Farm.Systems
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private CancellationTokenSource _velocityCt;
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
        }

        private void FixedUpdate()
        {
            IsMoving = _shouldMove && Vector3.Distance(_destination, transform.position) > _targetDistance;
            if (IsMoving)
            {
                var lookAtTarget = _destination;
                lookAtTarget.y = 0;
                transform.LookAt(lookAtTarget);
                _rigidbody.velocity = ((_destination - transform.position).normalized * Time.fixedDeltaTime * Speed);
            }
            else
            {
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
