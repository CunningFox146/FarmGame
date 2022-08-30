using Farm.InputActions;
using Farm.UI;
using Farm.UI.Elements;
using Zenject;

namespace Farm.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindViewSystem();
            BindHoldSystem();
            BindInputActions();
        }

        private void BindInputActions()
        {
            Container.Bind<UIInputActions>().AsSingle();
        }

        private void BindHoldSystem()
        {
            Container.Bind<ElementControlSystem>()
                .AsSingle()
                .NonLazy();
        }
        private void BindViewSystem()
        {
            Container.Bind<ViewSystem>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
