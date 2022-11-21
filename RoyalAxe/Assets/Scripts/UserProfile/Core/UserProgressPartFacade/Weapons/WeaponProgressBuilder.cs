using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProfileProgressFacade : UserProfileProgressFacade<UserAllWeaponsProgress>
    {
        protected override string Key => "Weapons";
        public WeaponProfileProgressFacade(IUserProgressPartFactory<UserAllWeaponsProgress> loader, IUserProfileProgressEntityBuilder<UserAllWeaponsProgress> userProfileProgressEntityBuilder, IUserProfileProgressHarvester<UserAllWeaponsProgress> harvester) : base(loader, userProfileProgressEntityBuilder, harvester) { }
    }
    
    public class WeaponProfileProgressEntityBuilder : IUserProfileProgressEntityBuilder<UserAllWeaponsProgress>
    {
        public IUserProgressProfile BuildGameEntity(UserAllWeaponsProgress progressData)
        {
            return new UserProfileWeaponsProgress(progressData); 
        }
    }
    

}