using Farm.InputActions;
using UnityEngine;
using Zenject;

namespace Farm.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            BindInputActions();
            BindMainCamera();
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
