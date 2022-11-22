using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProfileProgressFacade : UserProfileProgressFacade<UserLevelProgress>,IUserLevelsProgress
    {
        protected override string Key => "Levels";
        
        public LevelProfileProgressFacade(IUserProgressPartFactory<UserLevelProgress> loader) : base(loader) { }
        public LastLevel SavedLevel => Progress.LastSavedLevel;
        public LastLevel LastPlayed => Progress.LastPlayedLevel;

        public void UpdateLastPlayedLevel(LevelGeneratorSettings levelData)
        {
            var level = new LastLevel() {StartLevel = levelData.LevelNumber, Biome = levelData.Type};
            Progress.LastPlayedLevel = level;
            if (levelData.IsSafePoint)
                Progress.LastSavedLevel = level;
            SetDirty();
        }
        
        protected override void UpdateProgressData()
        {
        }

        protected override void SetToMainFacade(CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.LevelProgressFacade = this;
        }
    }
}
