using UnityEngine;

namespace Farm.Shadows
{
    public class DropShadow : MonoBehaviour
    {
        private DropShadowSystem _shadowSystem;

        public void Init(DropShadowSystem shadowSystem)
        {
            _shadowSystem = shadowSystem;
        }

        private void OnDestroy()
        {
            _shadowSystem.OnShadowDisabled(transform);
        }
    }
}
