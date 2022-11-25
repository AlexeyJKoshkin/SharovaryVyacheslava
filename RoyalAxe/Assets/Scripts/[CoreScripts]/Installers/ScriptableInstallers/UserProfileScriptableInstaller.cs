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
            BindSingletonWithAllInterfaces<DefaultProgressFactoryComposite>();
            BindSingletonWithAllInterfaces<UserProgressLoader>();


            BindSingletonWithAllInterfaces<HeroProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<HeroProfileProgressFacade>();


            BindSingletonWithAllInterfaces<LevelProgressBuilderDefault>();
            BindSingletonWithAllInterfaces<LevelProfileProgressFacade>();


            BindSingletonWithAllInterfaces<WeaponProgressBuilderDefault>();
            BindSingletonWithAllInterfaces<WeaponProfileProgressFacade>();


            BindSingletonWithAllInterfaces<GeneralProfileProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<GeneralProfileProgressFacade>();

            BindSingletonWithAllInterfaces<InventoryProfileProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<InventoryProfileProgressFacade>();
        }
    }
}