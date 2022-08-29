using Farm.InputActions;
using Farm.UI;
using System;
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
            BindViewSystem();
        }

        private void BindInputActions()
        {
            Container.Bind<PlayerInputActions>().AsSingle();
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
