using UnityEngine;

namespace Farm.Animations.FaceAppliers
{
    public class TwoFaceApplier : IFaceApplier
    {
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        public TwoFaceApplier(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _transform = spriteRenderer.transform;
        }

        public void ApplyFace()
        {
            _spriteRenderer.flipX = Mathf.Abs(_transform.eulerAngles.y) > 180;
        }
    }
}
