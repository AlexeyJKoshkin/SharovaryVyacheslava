namespace Core.UserProfile
{
    public class LevelProfileProgressFacade : UserProfileProgressFacade<UserLevelProgress>
    {
        protected override string Key => "Levels";
        public LevelProfileProgressFacade(IUserProgressPartFactory<UserLevelProgress> loader, IUserProfileProgressEntityBuilder<UserLevelProgress> userProfileProgressEntityBuilder, IUserProfileProgressHarvester<UserLevelProgress> harvester) : base(loader, userProfileProgressEntityBuilder, harvester) { }
    }
}
