using Core.UserProfile;
using Entitas;

namespace RoyalAxe.CoreLevel
{
    public class SaveTempProgressEachLevel : BaseSaveUserProgressAtLevels
    {
        public SaveTempProgressEachLevel(IContext<CoreGamePlayEntity> context,
                                         GameRootLoopContext gameRootLoopContext,
                                         ICurrentUserProgressProfileFacade userProgressProfileFacade) : base(context, userProgressProfileFacade, gameRootLoopContext) { }

        

        protected override bool CheckCanSave(CoreGamePlayEntity entity)
        {
            return true;
        }

        protected override void Execute(CoreGamePlayEntity e)
        {
            UserProgressProfileFacade.LevelProgressFacade.LastPlayed = e.currentLevelInfo.Level;
        }
    }
}
