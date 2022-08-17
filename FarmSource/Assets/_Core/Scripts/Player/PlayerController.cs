using Farm.InputActions;
using Farm.Interactable;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Farm.Player
{
    [RequireComponent(typeof(InteractionsSystem))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private InteractionsSystem _interactionsSystem;

        private void Awake()
        {
            _interactionsSystem = GetComponent<InteractionsSystem>();

            _inputActions = new PlayerInputActions();
            _inputActions.Player.Interact.performed += OnInteractHandler;
        }

        private void OnEnable()
        {
            EnablePlayerInput();
        }

        private void OnDisable()
        {
            DisablePlayerInput();
        }

        private void OnInteractHandler(InputAction.CallbackContext context)
        {
            var pos = _inputActions.Player.Position.ReadValue<Vector2>();
            var ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                var info = new InteractionInfo()
                {
                    Point = hit.point
                };
                _interactionsSystem.Interact(hit.transform.gameObject, info);
            }
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
