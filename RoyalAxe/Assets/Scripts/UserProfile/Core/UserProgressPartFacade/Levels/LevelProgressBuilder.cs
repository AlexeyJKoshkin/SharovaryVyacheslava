using Core.Configs;

namespace Core.UserProfile
{
    public class LevelProgressBuilder : IUserProfileProgressEntityBuilder<UserLevelProgress>
    {
        public IUserProgressProfile BuildGameEntity(UserLevelProgress progressData)
        {
            return new UserProfileLevelsProgress()
            {
                LastLevel = progressData.LastLevel
            };
        }
    }
}
