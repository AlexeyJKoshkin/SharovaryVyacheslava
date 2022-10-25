using Core;
using Core.Launcher;
using VContainer;

namespace RoyalAxe
{
    public class StartSceneInstaller : MonoInstaller
    {
        protected override void InstallBindings()
        {
            Container.Register<StartSceneState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<StateInfrastructure>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
