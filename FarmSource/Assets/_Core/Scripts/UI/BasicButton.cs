using System;
using UnityEngine.UI;

namespace Farm.UI
{
    public class BasicButton : Button, IHoldHandler
    {
        public event Action OnClick;
        public event Action OnHold;

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
    }
}
