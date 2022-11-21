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
            Container.Register<UserProfileDataCompositeBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            BindProgressBuilder();
        }

        private void BindProgressBuilder()
        {
            Container.Register<HeroProgressBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WeaponProgressBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelProgressBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.Register<HeroProgressBuilderDefaultBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WeaponProgressBuilderDefault>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelProgressBuilderDefault>(Lifetime.Singleton).AsImplementedInterfaces();

            Container.Register<UserProgressProfileFacade<UserAllWeaponsProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressProfileFacade<UserLevelProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressProfileFacade<UserAllHeroesProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.Register<UserProgressLoader<UserAllWeaponsProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressLoader<UserLevelProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressLoader<UserAllHeroesProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            
         



        }
    }
}