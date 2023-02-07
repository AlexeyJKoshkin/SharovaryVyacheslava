using Core.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/UnityBridge", fileName = "UnityBridgeInstaller")]
    public class UnityBridgeComponentInstaller : ScriptableInstaller
    {
        protected override void InstallBindings()
        {
            Container.RegisterComponentOnNewGameObject<GameRootUnityCallbackReceiver>(Lifetime.Singleton, "[MainFrame]").AsSelf();
                   

            Container.Register<RoyalAxePauseSystemSwitcher>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}