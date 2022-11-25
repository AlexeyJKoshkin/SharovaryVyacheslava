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
            BindSingletonWithAllInterfaces<HeroProfileProgressSaveLoaderAdapter>();
            BindSingletonWithAllInterfaces<HeroProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<UserProgressLoader<UserAllHeroesProgress>>();
            
            BindSingletonWithAllInterfaces<LevelProfileProgressSaveLoaderAdapter>();
            BindSingletonWithAllInterfaces<LevelProgressBuilderDefault>();
            BindSingletonWithAllInterfaces<UserProgressLoader<UserLevelProgress>>();
            
            BindSingletonWithAllInterfaces<WeaponProgressBuilderDefault>();
            BindSingletonWithAllInterfaces<WeaponProfileProgressSaveLoaderAdapter>();
            BindSingletonWithAllInterfaces<UserProgressLoader<UserAllWeaponsProgress>>();
            
            BindSingletonWithAllInterfaces<GeneralProfileProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<GeneralProfileProgressSaveLoaderAdapter>();
            BindSingletonWithAllInterfaces<UserProgressLoader<GeneralProfileProgress>>();
            
            BindSingletonWithAllInterfaces<InventoryProfileProgressDefaultFactory>();
            BindSingletonWithAllInterfaces<InventoryProfileProgressSaveLoaderAdapter>();
            BindSingletonWithAllInterfaces<UserProgressLoader<ProfileInventoryProgress>>();
        }
    }
}