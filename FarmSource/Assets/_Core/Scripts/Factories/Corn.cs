using Farm.Factories;
using Zenject;

namespace Farm.GrowSystem
{
    public class Corn : Plant
    {
        [Inject]
        private void Constructor(TestPickable.Factory factory)
        {
            _collectable.ProductFactory = factory;
        }

        public class Factory : PlaceholderFactory<Corn> { }
    }
}
