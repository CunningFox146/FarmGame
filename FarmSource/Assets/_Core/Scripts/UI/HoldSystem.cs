using Farm.InputActions;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using UnityEngine;

namespace Farm.UI
{
    public class HoldSystem : IDisposable
    {
        private UIInputActions _inputActions;
        private ViewSystem _viewSystem;

        public HoldSystem(UIInputActions inputActions, ViewSystem viewSystem)
        {
            _inputActions = inputActions;
            _viewSystem = viewSystem;
            _inputActions.UI.Enable();

            _inputActions.UI.ButtonHold.performed += OnButtonHoldHandler;
        }

        private void OnButtonHoldHandler(InputAction.CallbackContext context)
        {
            var pos = _inputActions.UI.Position.ReadValue<Vector2>();
            foreach (var item in _viewSystem.GetElementsAtPoint(pos))
            {
                if (item.gameObject.TryGetComponent<IHoldHandler>(out IHoldHandler holdHandler))
                {
                    holdHandler.OnHoldHandler();
                }
            }
        }

        public void Dispose()
        {
            _inputActions.Dispose();
        }
    }
}
