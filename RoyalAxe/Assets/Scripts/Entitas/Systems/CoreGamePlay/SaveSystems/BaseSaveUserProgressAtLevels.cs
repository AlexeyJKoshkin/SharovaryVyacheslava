using Core.UserProfile;
using Entitas;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.CoreLevel
{
    public abstract class BaseSaveUserProgressAtLevels : RAReactiveSystem<CoreGamePlayEntity>
    {
        protected readonly ICurrentUserProgressProfileFacade UserProgressProfileFacade;
        private readonly GameRootLoopContext _gameRootLoopContext;

        public BaseSaveUserProgressAtLevels(IContext<CoreGamePlayEntity> context,
                                            ICurrentUserProgressProfileFacade userProgressProfileFacade, GameRootLoopContext gameRootLoopContext) : base(context)
        {
            UserProgressProfileFacade = userProgressProfileFacade;
            _gameRootLoopContext = gameRootLoopContext;
        }

        protected sealed override bool Filter(CoreGamePlayEntity entity)
        {
            if (_gameRootLoopContext.isCheats) return false;
            return CheckCanSave(entity);
        }

        protected abstract bool CheckCanSave(CoreGamePlayEntity entity);

        protected sealed override ICollector<CoreGamePlayEntity> GetTrigger(IContext<CoreGamePlayEntity> context)
        {
            return context.CreateCollector(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayMatcher.CurrentLevelInfo,
                                                                             CoreGamePlayMatcher.LevelWave).Added());
        }
    }
}
