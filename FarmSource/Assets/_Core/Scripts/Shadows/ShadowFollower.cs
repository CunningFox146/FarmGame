using UnityEngine;

namespace Farm.Shadows
{
    public class ShadowFollower : MonoBehaviour
    {
        private ShadowSystem _shadowSystem;

        public void Init(ShadowSystem shadowSystem)
        {
            _shadowSystem = shadowSystem;
        }

        private void OnDestroy()
        {
            _shadowSystem.OnShadowDisabled(transform);
        }
    }
}
