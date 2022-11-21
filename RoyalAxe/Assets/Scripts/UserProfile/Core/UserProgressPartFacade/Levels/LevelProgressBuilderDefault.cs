using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProgressBuilderDefault : IDefaultProgressFactory<UserLevelProgress>
    {
        public UserLevelProgress CreateDefault()
        {
            return new UserLevelProgress()
            {
                LastLevel = new LastLevel()
                {
                    Biome = BiomeType.Forest,
                    StartLevel = 1
                }
            };
        }
    }
}
