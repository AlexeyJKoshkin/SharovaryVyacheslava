using Entitas;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.Entitas.Systems.TimersSystem
{
    public class DoneTimerSystem : RAReactiveSystem<GameRootLoopEntity>
    {
        public DoneTimerSystem(IContext<GameRootLoopEntity> context) : base(context) { }

        protected override ICollector<GameRootLoopEntity> GetTrigger(IContext<GameRootLoopEntity> context)
        {
            return context.CreateCollector(Matcher<GameRootLoopEntity>.AllOf(GameRootLoopMatcher.Timer, GameRootLoopMatcher.DoneTimer)
                                                                      .NoneOf(GameRootLoopMatcher.Pause).Added());
        }

        protected override bool Filter(GameRootLoopEntity entity)
        {
            return true;
        }

        protected override void Execute(GameRootLoopEntity entity)
        {
            entity.isDoneTimer = false;
            if (entity.isRepeat)
            {
                entity.ReplaceTimer(0, entity.timer.DestinationTime);
            }
            else
            {
                entity.Destroy();
            }
        }
    }
}