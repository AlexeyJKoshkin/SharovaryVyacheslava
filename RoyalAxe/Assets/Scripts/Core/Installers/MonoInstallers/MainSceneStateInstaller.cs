using Core.Launcher;
using ProjectUI;
using UnityEngine;
using VContainer;

namespace Core
{
    public class MainSceneStateInstaller : MonoInstaller
    {
        [SerializeField] private MainSceneUIView _ui;

        protected override void InstallBindings()
        {
            Container.Register<MainStateMetaScene>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<MaineSceneStateProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.RegisterInstance(_ui).AsSelf();
        }
    }
}