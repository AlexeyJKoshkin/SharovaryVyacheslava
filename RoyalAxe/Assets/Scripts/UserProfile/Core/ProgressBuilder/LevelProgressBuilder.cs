using Core.Configs;
using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProgressBuilder : UserProfileInfrastructureHelper<UserLevelProgress>
    {
        public override string FileName => "Levels";
        public override UserLevelProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.LevelProgress;
        }

        public override void SetItemToResult(UserProfileData result, UserLevelProgress item)
        {
            result.LevelProgress = item;
        }
    }
    
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
