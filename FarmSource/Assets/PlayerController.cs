using UnityEngine;
using UnityEngine.AI;

namespace Farm.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                var camera = Camera.main;
                var ray = camera.ScreenPointToRay(mousePos);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    _navMeshAgent.SetDestination(hit.point);
                }
            }
        }
    }
}