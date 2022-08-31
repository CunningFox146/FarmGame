using Farm.Billboard.FaceAppliers;
using UnityEngine;

namespace Farm.Billboard
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BillboardSprite : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private IFaceApplier _faceApplier;
        private BillboardSystem _billboardSystem;

        [field: SerializeField] public BillboardFaces Faces { get; private set; }

        public Material SpriteMaterial
        {
            get => _spriteRenderer.material;
            private set => _spriteRenderer.material = value;
        }

        [Zenject.Inject]
        private void Constructor(BillboardSystem billboardSystem)
        {
            _billboardSystem = billboardSystem;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SpriteMaterial = new Material(_spriteRenderer.material);
            InitFaceApplier();
        }

        private void OnEnable()
        {
            _billboardSystem.RegisterBillboard(transform);
        }

        private void OnDisable()
        {
            _billboardSystem.UnregisterBillboard(transform);
        }

        private void Update()
        {
            _faceApplier?.ApplyFace();
        }

        private void InitFaceApplier()
        {
            switch (Faces)
            {
                case BillboardFaces.TwoFaced:
                    _faceApplier = new TwoFaceApplier(_spriteRenderer);
                    break;
                default:
                    _faceApplier = null;
                    break;
            }
        }
    }
}
