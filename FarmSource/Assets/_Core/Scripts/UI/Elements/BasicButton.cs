using System;
using UnityEngine.UI;

namespace Farm.UI.Elements
{
    public class BasicButton : Button, IHoldHandler, IDoubleTapHandler
    {
        public event Action OnClick;
        public event Action OnHold;
        public event Action OnDoubleTap;

        protected override void OnEnable()
        {
            base.OnEnable();

            onClick.AddListener(OnClickHandler);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            onClick.RemoveListener(OnClickHandler);
        }

        protected virtual void OnClickHandler()
        {
            OnClick?.Invoke();
        }

        public void OnHoldHandler()
        {
            OnHold?.Invoke();
        }

        public void OnDoubleTapHandler()
        {
            OnDoubleTap?.Invoke();
        }
    }
}
