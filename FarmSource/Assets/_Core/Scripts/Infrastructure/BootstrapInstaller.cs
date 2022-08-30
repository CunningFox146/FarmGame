using Farm.Animations;
using Zenject;

namespace Farm.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RegisterBillboardSystem();
        }

        private void RegisterBillboardSystem()
        {
            Container.BindInterfacesAndSelfTo<BillboardSystem>()
                .AsSingle();
        }
    }
}
