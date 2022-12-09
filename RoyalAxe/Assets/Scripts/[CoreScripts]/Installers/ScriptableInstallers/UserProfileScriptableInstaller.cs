#region

using Core.UserProfile;
using UnityEngine;
using VContainer;

#endregion

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/UserProfileScriptable", fileName = "UserProfileScriptableInstaller")]
    public class UserProfileScriptableInstaller : ScriptableInstaller
    {
        [SerializeField] private UserSaveSettingStorage _settings;

        protected override void InstallBindings()
        {
            Container.RegisterInstance(_settings.GetCurrentSettings()).AsSelf();
            Container.Register<UserSaveProfileStorage>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<ProfileProgressPartContextFactory>(Lifetime.Singleton).AsImplementedInterfaces();


            BindProgressBuilder();
        }

        private void BindProgressBuilder()
        {
            SingletonAllInterfaces<DefaultProgressFactoryComposite>();
            SingletonAllInterfaces<UserProgressLoader>();


            SingletonAllInterfaces<HeroProgressDefaultFactory>();
            SingletonAllInterfaces<HeroProfileProgressFacade>();


            SingletonAllInterfaces<LevelProgressBuilderDefault>();
            SingletonAllInterfaces<LevelProfileProgressFacade>();


            SingletonAllInterfaces<WeaponProgressBuilderDefault>();
            SingletonAllInterfaces<WeaponProfileProgressFacade>();


            SingletonAllInterfaces<GeneralProfileProgressDefaultFactory>();
            SingletonAllInterfaces<GeneralProfileProgressFacade>();

            SingletonAllInterfaces<InventoryProfileProgressDefaultFactory>();
            SingletonAllInterfaces<InventoryProfileProgressFacade>();
        }
    }
}