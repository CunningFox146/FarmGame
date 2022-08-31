using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Farm.Shadows
{
    public class DropShadowSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _shadowPrefab;
        [SerializeField] private int _poolSize;
        private ObjectPool<Transform> _pool;

        private Dictionary<Transform, Transform> _trackedObjects = new();

        private void Awake()
        {
            _pool = new(
                () => {
                    var shadow = Instantiate(_shadowPrefab, transform);
                    shadow.GetComponent<DropShadow>().Init(this);
                    return shadow.transform;
                },
                (shadow) => shadow.gameObject.SetActive(true),
                (shadow) => shadow.gameObject.SetActive(false),
                null,
                true,
                _poolSize
            );
        }

        private void Update()
        {
            foreach (KeyValuePair<Transform, Transform> pair in _trackedObjects)
            {
                var target = pair.Key;
                var shadow = pair.Value;
                var targetPos = target.position;
                targetPos.y = 0f;
                shadow.position = targetPos;
            }
        }

        public void RegisterShadow(Transform target, Vector2 shadowSize)
        {
            if (_pool is null || _trackedObjects.ContainsKey(target)) return;
            var shadow = _pool.Get();
            shadow.localScale = new Vector3(shadowSize.x, 1f, shadowSize.y);
            _trackedObjects[target] = shadow;
        }

        public void UnregisterShadow(Transform target)
        {
            if (_pool is null || !_trackedObjects.ContainsKey(target)) return;
            var shadow = _trackedObjects[target];
            _trackedObjects.Remove(target);
            _pool.Release(shadow);
        }

        public void OnShadowDisabled(Transform shadowTransform)
        {
            foreach (KeyValuePair<Transform, Transform> pair in _trackedObjects)
            {
                var target = pair.Key;
                var shadow = pair.Value;
                if (shadowTransform == shadow)
                {
                    _trackedObjects.Remove(target);
                    break;
                }
            }
        }
    }
}
