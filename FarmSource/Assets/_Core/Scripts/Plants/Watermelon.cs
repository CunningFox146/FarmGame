using Farm.Factories;
using UnityEngine;
using Zenject;

namespace Farm.Plants
{
    public class Watermelon : AnimatedPlant
    {
        [SerializeField] private GameObject _watermelonProduct;

        [Inject]
        private void Constructor(TestPickable.Factory factory)
        {
            _collectable.ProductFactory = factory;
        }
        private void Start()
        {
            StageUpdate += OnStageUpdateHandler;
        }

        private void OnStageUpdateHandler()
        {
            if (IsFull)
            {
                _watermelonProduct.SetActive(true);
            }
        }

        public class Factory : PlaceholderFactory<Watermelon> { }
    }
}
