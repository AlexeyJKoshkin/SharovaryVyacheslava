using Core;
using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class UnitsMovingSystem : IExecuteSystem, IInitializeSystem, IGamePlaySceneSystem
    {
        private readonly UnitsContext _unitsContext;
        private IGroup<UnitsEntity> _movingMobs;

        public UnitsMovingSystem(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
        }

        public void Execute()
        {
            var entities = _movingMobs.GetEntities();

            for (int i = 0; i < entities.Length; i++) UpdateMobPosition(entities[i]);
        }

        private void UpdateMobPosition(UnitsEntity entity)
        {
            var transform = entity.unitsView.View.RootTransform;
            Vector2 currentPos = transform.position;
            transform.position = Vector2.MoveTowards(currentPos, entity.movingToPoint.TargetPosition, entity.moveSpeed.CurrentValue * Time.deltaTime);
            entity.ReplaceMovingToPoint(entity.movingToPoint.PointAdapter);
        }

        public void Initialize()
        {
            _movingMobs = _unitsContext.GetGroup(UnitsMatcherLibrary.MovingUnits().Matcher());
        }
    }

    public class SetPauseNavMeshSystem : IGamePauseListener, IInitializeSystem
    {
        private readonly UnitsContext _unitsContext;
        private readonly GameRootLoopContext _gameRootContext;
        private IGroup<UnitsEntity> _movingMobs;
        public SetPauseNavMeshSystem(UnitsContext unitsContext, GameRootLoopContext gameRootContext)
        {
            _unitsContext = unitsContext;
            _gameRootContext = gameRootContext;
        } 
        public void OnGamePause(GameRootLoopEntity entity, bool isPause)
        {
            /*HLogger.TempLog(isPause);
            foreach (var unit in _movingMobs.AsEnumerable())
            {
                unit.navMeshAgent.NavMeshAgent.enabled = !isPause;
            }*/
        }

        public void Initialize()
        {
            _movingMobs = _unitsContext.GetGroup(UnitsMatcherLibrary.MovingNavMeshUnits().Matcher());
            _gameRootContext.gamePauseEntity.AddGamePauseListener(this);
        }
    }
}
