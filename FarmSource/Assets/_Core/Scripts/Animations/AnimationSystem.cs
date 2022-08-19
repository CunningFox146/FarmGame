using UnityEngine;

namespace Farm.Animations
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class AnimationSystem : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private IFaceApplier _faceApplier;
        [field: SerializeField] public AnimationFaces Faces { get; private set; }

        public Material SpriteMaterial
        {
            get => _spriteRenderer.material;
            private set => _spriteRenderer.material = value;
        }

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SpriteMaterial = new Material(_spriteRenderer.material);
            InitFaceApplier();
        }

        public void Play(int name, float transitionTime = 0f, int layer = 0)
        {
            _animator.CrossFade(name, transitionTime, layer);
        }

        private void InitFaceApplier()
        {
            switch (Faces)
            {
                case AnimationFaces.TwoFaced:
                    _faceApplier = new TwoFaceApplier(_spriteRenderer);
                    break;
                default:
                    _faceApplier = null;
                    break;
            }
        }

        private void Update()
        {
            _faceApplier?.ApplyFace();
        }
    }
}