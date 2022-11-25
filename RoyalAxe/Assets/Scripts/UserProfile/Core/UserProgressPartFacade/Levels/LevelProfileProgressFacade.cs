using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    public class LevelProfileProgressSaveLoaderAdapter : UserProfileProgressSaveLoaderAdapter<UserLevelProgress>,IUserLevelsProgress
    {
        protected override string Key => "Levels";
        
        public LevelProfileProgressSaveLoaderAdapter(IUserProgressPartFactory<UserLevelProgress> loader) : base(loader) { }
        public LastLevel SavedLevel   {
            get { return Progress.LastSavedLevel; }
            set
            {
                Progress.LastSavedLevel = value;
                SetDirty();
            }
        } 
        public LastLevel LastPlayed
        {
            get { return Progress.LastPlayedLevel; }
            set
            {
                Progress.LastPlayedLevel = value;
                SetDirty();
            }
        } 
        

        protected override void SetToMainFacade(GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.LevelProgressFacade = this;
        }
    }
}
