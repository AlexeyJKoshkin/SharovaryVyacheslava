using Core;
using Entitas;
using GameKit;
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
            _movingMobs.AsEnumerable().ForEach(UpdateMobPosition);
            
            
            void UpdateMobPosition(UnitsEntity entity)
            {
                var     transform  = entity.unitsView.View.RootTransform;
                Vector2 currentPos = transform.position;
                transform.position = Vector2.MoveTowards(currentPos, entity.movingToPoint.TargetPosition, entity.moveSpeed.CurrentValue * Time.deltaTime);
                entity.ReplaceMovingToPoint(entity.movingToPoint.PointAdapter);
            }
        }


        public void Initialize()
        {
            _movingMobs = _unitsContext.GetGroup(UnitsMatcherLibrary.MovingSimpleUnits().Matcher());
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
            foreach (var unit in _movingMobs.AsEnumerable())
            {
                unit.navMeshAgent.NavMeshAgent.isStopped = !isPause;
            }
        }

        public void Initialize()
        {
            _movingMobs = _unitsContext.GetGroup(UnitsMatcherLibrary.MovingNavMeshUnits().Matcher());
            _gameRootContext.gamePauseEntity.AddGamePauseListener(this);
        }
    }
}
