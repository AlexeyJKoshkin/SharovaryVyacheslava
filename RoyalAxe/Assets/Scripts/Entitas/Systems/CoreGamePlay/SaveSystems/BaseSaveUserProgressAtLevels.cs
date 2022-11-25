using Core.UserProfile;
using Entitas;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.CoreLevel
{
    public abstract class BaseSaveUserProgressAtLevels : RAReactiveSystem<CoreGamePlayEntity>
    {
        protected readonly ICurrentUserProgressProfileFacade UserProgressProfileFacade;
        public BaseSaveUserProgressAtLevels(IContext<CoreGamePlayEntity> context, ICurrentUserProgressProfileFacade userProgressProfileFacade) : base(context)
        {
            UserProgressProfileFacade = userProgressProfileFacade;
        }
        protected sealed override ICollector<CoreGamePlayEntity> GetTrigger(IContext<CoreGamePlayEntity> context)
        {
            return context.CreateCollector(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayMatcher.CurrentLevelInfo, CoreGamePlayMatcher.LevelWave)
                                                                      .Added());
        }
    }
}
