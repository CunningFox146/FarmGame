using Farm.Billboard;
using UnityEngine;

namespace Farm.Interactable
{
    [RequireComponent(typeof(BoxCollider))]
    public class Mouseover : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _billboardSprite;
        private BoxCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            if (_billboardSprite is not null)
            {
                RecalculateSize(_billboardSprite);
            }
        }

        private void OnEnable()
        {
            _billboardSprite?.RegisterSpriteChangeCallback(RecalculateSize);
        }

        private void OnDisable()
        {
            _billboardSprite?.UnregisterSpriteChangeCallback(RecalculateSize);
        }

        private void RecalculateSize(SpriteRenderer renderer)
        {
            var spriteSize = renderer.bounds.size;
            var size = new Vector3(spriteSize.x, spriteSize.y, .1f);

            _collider.size = size;
            _collider.center = Vector3.up * 0.5f * size.y;
        }
    }
}
