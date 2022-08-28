using UnityEngine.UI;

namespace Farm.UI
{
    public class FarmButton : Button
    {
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
            // TODO: Play sound and do some fancy stuff
        }

    }
}
