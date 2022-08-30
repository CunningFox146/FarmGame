using Farm.InputActions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Farm.UI.Elements
{
    public class ElementControlSystem : IDisposable
    {
        private UIInputActions _inputActions;
        private ViewSystem _viewSystem;

        public ElementControlSystem(UIInputActions inputActions, ViewSystem viewSystem)
        {
            _inputActions = inputActions;
            _viewSystem = viewSystem;
            _inputActions.UI.Enable();

            _inputActions.UI.ButtonHold.performed += OnButtonHoldHandler;
            _inputActions.UI.DoubleTap.performed += OnDoubleTapHandler;
        }

        private List<UnityEngine.EventSystems.RaycastResult> GetElements()
        {
            var pos = _inputActions.UI.Position.ReadValue<Vector2>();
            return _viewSystem.GetElementsAtPoint(pos);
        }

        private void OnButtonHoldHandler(InputAction.CallbackContext context)
        {
            foreach (var item in GetElements())
            {
                if (item.gameObject.TryGetComponent(out IHoldHandler holdHandler))
                {
                    holdHandler.OnHoldHandler();
                }
            }
        }

        private void OnDoubleTapHandler(InputAction.CallbackContext context)
        {
            foreach (var item in GetElements())
            {
                if (item.gameObject.TryGetComponent(out IDoubleTapHandler holdHandler))
                {
                    holdHandler.OnDoubleTapHandler();
                }
            }
        }

        public void Dispose()
        {
            _inputActions.Dispose();
        }
    }
}
