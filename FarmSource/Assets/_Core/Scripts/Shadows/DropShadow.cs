using UnityEngine;

namespace Farm.Shadows
{
    public class DropShadow : MonoBehaviour
    {
        [SerializeField] private Vector2 _shadowSize = Vector2.one;

        private ShadowSystem _shadowSystem;

        [Zenject.Inject]
        private void Constructor(ShadowSystem shadowSystem)
        {
            _shadowSystem = shadowSystem;
        }

        private void Start()
        {
            EnableShadow();
        }

        private void OnEnable()
        {
            EnableShadow();
        }

        private void OnDisable()
        {
            DisableShadow();
        }

        private void EnableShadow()
        {
            _shadowSystem.RegisterShadow(transform, _shadowSize);
        }

        private void DisableShadow()
        {
            if (!enabled) return;
            _shadowSystem.UnregisterShadow(transform);
        }
    }
}
