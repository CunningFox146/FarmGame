using Farm.InputActions;
using Farm.Shadows;
using UnityEngine;
using Zenject;

namespace Farm.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            BindShadowSystem();
            BindInputActions();
            BindMainCamera();
        }

        private void BindShadowSystem()
        {
            Container.Bind<ShadowSystem>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void BindInputActions()
        {
            Container.Bind<PlayerInputActions>().AsSingle();
        }

        private void BindMainCamera()
        {
            Container.Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();
        }
    }
}
