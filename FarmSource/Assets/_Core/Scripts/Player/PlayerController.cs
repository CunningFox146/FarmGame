using Farm.InputActions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Farm.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Interact.performed += OnInteractHandler;
        }

        private void OnInteractHandler(InputAction.CallbackContext context)
        {
            var pos = _inputActions.Player.Position.ReadValue<Vector2>();
            var ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }

        private void OnEnable()
        {
            EnablePlayerInput();
        }

        private void OnDisable()
        {
            DisablePlayerInput();
        }

        private void DisablePlayerInput()
        {
            _inputActions.Player.Disable();
        }

        private void EnablePlayerInput()
        {
            _inputActions.Player.Enable();
        }
    }
}
