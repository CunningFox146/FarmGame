using Farm.Billboard.FaceAppliers;
using UnityEngine;

namespace Farm.Billboard
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BillboardSprite : MonoBehaviour
    {
        private IFaceApplier _faceApplier;
        private BillboardSystem _billboardSystem;

        [field: SerializeField] public BillboardFaces Faces { get; private set; }

        public SpriteRenderer SpriteRenderer { get; private set; }
        public Material SpriteMaterial
        {
            get => SpriteRenderer.material;
            private set => SpriteRenderer.material = value;
        }

        [Zenject.Inject]
        private void Constructor(BillboardSystem billboardSystem)
        {
            _billboardSystem = billboardSystem;
        }

        private void Reset()
        {
            SetupSpriteRenderer();
        }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();

            SpriteMaterial = new Material(SpriteRenderer.material);
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

        private void SetupSpriteRenderer()
        {
            if (SpriteRenderer is null)
            {
                SpriteRenderer = GetComponent<SpriteRenderer>();
            }
            SpriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        }

        private void InitFaceApplier()
        {
            switch (Faces)
            {
                case BillboardFaces.TwoFaced:
                    _faceApplier = new TwoFaceApplier(SpriteRenderer);
                    break;
                default:
                    _faceApplier = null;
                    break;
            }
        }
    }
}
