using Farm.InputActions;
using Farm.UI;
using UnityEngine;
using Zenject;

namespace Farm.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            BindHoldSystem();
            BindInputActions();
            BindMainCamera();
            BindViewSystem();
        }

        private void BindHoldSystem()
        {
            Container.Bind<HoldSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInputActions()
        {
            Container.Bind<PlayerInputActions>().AsSingle();
            Container.Bind<UIInputActions>().AsSingle();
        }

        private void BindViewSystem()
        {
            Container.Bind<ViewSystem>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void BindMainCamera()
        {
            Container.Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();
        }
    }
}
