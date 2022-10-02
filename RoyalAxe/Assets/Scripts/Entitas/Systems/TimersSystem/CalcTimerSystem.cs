using Entitas;
using UnityEngine;

namespace RoyalAxe.Entitas.Systems.TimersSystem
{
    public class CalcTimerSystem : IExecuteSystem
    {
        private readonly IGroup<GameRootLoopEntity> _timersEntity;

        public CalcTimerSystem(GameRootLoopContext context)
        {
            _timersEntity = context.GetGroup(Matcher<GameRootLoopEntity>.AllOf(GameRootLoopMatcher.Timer, GameRootLoopMatcher.ActiveTimer).NoneOf(GameRootLoopMatcher.DoneTimer, GameRootLoopMatcher.Pause));
        }

        public void Execute()
        {
            var entities = _timersEntity.GetEntities();

            for (int i = 0; i < entities.Length; i++) UpdateTimer(entities[i]);
        }

        private void UpdateTimer(GameRootLoopEntity timer)
        {
            var old = timer.timer;
            timer.ReplaceTimer(old.Counter + Time.deltaTime, old.DestinationTime);
            if (timer.timer.IsDone)
            {
                timer.isDoneTimer = true;
            }
        }
    }
}