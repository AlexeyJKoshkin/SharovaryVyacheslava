using Core;
using Core.Launcher;
using ProjectUI;
using UnityEngine;
using VContainer;

namespace RoyalAxe
{
    public class MetaSceneStateInstaller : MonoInstaller
    {
        [SerializeField]
        private MetaSceneUIView _metaSceneUIView;

        protected override void InstallBindings()
        {
            Container.Register<MetaSceneState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<MetaSceneInfrastructure>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            Container.RegisterInstance(_metaSceneUIView);
            
            Container.Register<MainStateMetaScene>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<MaineSceneStateProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<StateLoaderProvider>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<TempMetaStateDirector>(Lifetime.Singleton).AsSelf();
        }
    }
}
