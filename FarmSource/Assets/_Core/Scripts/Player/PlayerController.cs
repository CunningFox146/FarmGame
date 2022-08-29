using Farm.InputActions;
using Farm.Interactable;
using Farm.UI;
using Farm.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Farm.Player
{
    [RequireComponent(typeof(InteractionsSystem))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance;
        private PlayerInputActions _inputActions;
        private InteractionsSystem _interactionsSystem;
        private Camera _mainCamera;
        private ViewSystem _viewSystem;

        [Zenject.Inject]
        private void Constructor(Camera camera, ViewSystem viewSystem, PlayerInputActions inputActions)
        {
            _inputActions = inputActions;
            _inputActions.Player.Interact.performed += OnInteractHandler;

            _mainCamera = camera;
            _viewSystem = viewSystem;
        }

        private void Awake()
        {
            _interactionsSystem = GetComponent<InteractionsSystem>();
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

            if (_viewSystem.IsPointerOnUI(pos)) return;

            var ray = _mainCamera.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance, 1 << (int)Layers.Interactable))
            {
                var info = new InteractionInfo()
                {
                    Point = new Vector3(hit.point.x, 0f, hit.point.z),
                };
                _interactionsSystem.Interact(hit.transform, info);
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
