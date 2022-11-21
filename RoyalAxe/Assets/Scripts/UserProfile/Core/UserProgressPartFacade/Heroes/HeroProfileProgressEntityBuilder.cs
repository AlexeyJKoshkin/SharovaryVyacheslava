namespace Core.UserProfile
{
    public class HeroProfileProgressEntityBuilder : IUserProfileProgressEntityBuilder<UserAllHeroesProgress>
    {
        public IUserProgressProfile BuildGameEntity(UserAllHeroesProgress progressData)
        {
            return new UserHeroesProgressProfile(progressData); 
        }
    }
}
