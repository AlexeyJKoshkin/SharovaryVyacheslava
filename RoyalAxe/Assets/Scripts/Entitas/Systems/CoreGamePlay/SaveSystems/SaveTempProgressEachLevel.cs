using Core;
using Core.UserProfile;
using Entitas;

namespace RoyalAxe.CoreLevel
{
    public class SaveTempProgressEachLevel : BaseSaveUserProgressAtLevels
    {
        public SaveTempProgressEachLevel(IContext<CoreGamePlayEntity> context, ICurrentUserProgressProfileFacade userProgressProfileFacade) : base(context, userProgressProfileFacade) { }

        protected override bool Filter(CoreGamePlayEntity entity)
        {
            return true;
        }

        protected override void Execute(CoreGamePlayEntity e)
        {
            HLogger.TempLog("Save temp"); // todo remove log
            //тут же надо сохранять гемы, опыт, всю всю поеботу во временный профиль-сейв
            UserProgressProfileFacade.LevelProgressFacade.LastPlayed = e.currentLevelInfo.Level;
        }
    }
}
