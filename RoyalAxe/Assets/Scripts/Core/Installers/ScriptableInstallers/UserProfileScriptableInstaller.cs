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
            Container.Register<HeroProfileProgressEntityBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<HeroProfileProgressFacade>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<HeroProgressDefaultFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressLoader<UserAllHeroesProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            Container.Register<LevelProfileProgressFacade>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelProgressBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelProgressBuilderDefault>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressLoader<UserLevelProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            
            Container.Register<WeaponProgressBuilderDefault>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WeaponProfileProgressFacade>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UserProgressLoader<UserAllWeaponsProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WeaponProfileProgressEntityBuilder>(Lifetime.Singleton).AsImplementedInterfaces();


            Container.Register<MockUserProfileProgressHarvester<UserAllHeroesProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MockUserProfileProgressHarvester<UserLevelProgress>>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MockUserProfileProgressHarvester<UserAllWeaponsProgress>>(Lifetime.Singleton).AsImplementedInterfaces();





        }
    }
}