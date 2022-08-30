using Farm.Animations;
using UnityEngine;
using Zenject;

namespace Farm.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Application.targetFrameRate = 300;
            RegisterBillboardSystem();
        }

        private void RegisterBillboardSystem()
        {
            Container.BindInterfacesAndSelfTo<BillboardSystem>()
                .AsSingle();
        }
    }
}
