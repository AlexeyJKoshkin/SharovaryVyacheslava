using Core.Launcher;
using UnityEngine;
using VContainer;

namespace Core 
{
    [CreateAssetMenu(menuName = "Installers/SceneLoaderViewInstaller", fileName = "SceneLoaderViewInstaller")]
    public class SceneLoaderViewInstaller : ScriptableInstaller
    {
        [SerializeField] private SceneLoaderUnityView _loaderUnityView;

        protected override void InstallBindings()
        {
            var loaderView = Instantiate(_loaderUnityView);
            DontDestroyOnLoad(loaderView);

            Container.RegisterInstance(loaderView).AsSelf().AsImplementedInterfaces();
            Container.Register<SceneLoaderState>(Lifetime.Singleton).As<ISceneLoader>().As<ISceneLoaderSceneState>();
        }
    }
}