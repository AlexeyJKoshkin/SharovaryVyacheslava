using Core.Configs;

namespace Core.UserProfile {
    public class HeroProfileProgressFacade : UserProfileProgressFacade<UserAllHeroesProgress>
    {
        protected override string Key => "Heroes";
        public HeroProfileProgressFacade(IUserProgressPartFactory<UserAllHeroesProgress> loader, IUserProfileProgressEntityBuilder<UserAllHeroesProgress> userProfileProgressEntityBuilder, IUserProfileProgressHarvester<UserAllHeroesProgress> harvester) : base(loader, userProfileProgressEntityBuilder, harvester) { }
    }
}