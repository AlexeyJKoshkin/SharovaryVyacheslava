using Core.Launcher;
using RoyalAxe.CoreLevel;
using UnityEngine;
using VContainer;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/SceneLoaderInstaller", fileName = "SceneLoaderInstaller")]
    public class SceneLoaderInstaller : ScriptableInstaller
    {
        protected override void InstallBindings()
        {
            Container.Register<CoreGameSceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<SceneLoaderProvider>(Lifetime.Singleton).AsImplementedInterfaces();
           
            
            Container.Register<CoreLevelDataInfrastructure>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}
