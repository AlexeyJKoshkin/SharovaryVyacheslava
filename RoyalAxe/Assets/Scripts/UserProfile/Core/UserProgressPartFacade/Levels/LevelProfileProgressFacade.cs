namespace Core.UserProfile
{
    public class LevelProfileProgressFacade : UserProgressPartFacade<UserLevelProgress>, IUserLevelsProgress
    {
        protected override string Key => "Levels";
        public LastLevel SavedLevel
        {
            get => Progress.LastSavedLevel;
            set
            {
                Progress.LastSavedLevel = value;
                SetDirty();
            }
        }
        public LastLevel LastPlayed
        {
            get => Progress.LastPlayedLevel;
            set
            {
                Progress.LastPlayedLevel = value;
                SetDirty();
            }
        }
        public LevelProfileProgressFacade(IUserProgressPartSaveLoader progressAdapter) : base(progressAdapter) { }
    }
}