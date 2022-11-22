using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProgressBuilderDefault : IDefaultProgressFactory<UserLevelProgress>
    {
        public UserLevelProgress CreateDefault()
        {
            return new UserLevelProgress()
            {
                LastPlayedLevel = LastLevel.Default,
                LastSavedLevel = LastLevel.Default
            };
        }
    }
}
