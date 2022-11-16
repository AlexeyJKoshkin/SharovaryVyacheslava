using Core.Configs;
using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProgressBuilder : UserProfilePartBuilder<UserLevelProgress>
    {
        public LevelProgressBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter, IDefaultProgressFactory<UserLevelProgress> defaultProgressFactory) : base(jsonFileOperation, jsonConverter, defaultProgressFactory) { }
        protected override string FileName => "Levels";
        protected override UserLevelProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.LevelProgress;
        }

        protected override void SetItemToResult(UserProfileData result, UserLevelProgress item)
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
