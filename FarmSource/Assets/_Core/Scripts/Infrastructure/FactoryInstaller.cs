using Farm.Factories;
using Farm.GrowSystem;
using UnityEngine;
using Zenject;

namespace Farm.Infrastructure
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private TestPickable _testPickable;
        [SerializeField] private Corn _corn;

        public override void InstallBindings()
        {
            //Container.BindFactory<Corn, Corn.Factory>().FromComponentInNewPrefab(_corn).AsSingle();
            Container.BindFactory<TestPickable, TestPickable.Factory>().FromComponentInNewPrefab(_testPickable).AsSingle();
        }
    }
}
