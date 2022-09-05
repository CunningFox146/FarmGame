﻿using Farm.Factories;
using Zenject;

namespace Farm.Plants
{
    public class Carrot : AnimatedPlant
    {
        [Inject]
        private void Constructor(TestPickable.Factory factory)
        {
            _collectable.ProductFactory = factory;
        }

        public class Factory : PlaceholderFactory<Carrot> { }
    }
}
