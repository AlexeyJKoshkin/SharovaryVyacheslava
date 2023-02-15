using Core;
using Entitas;
using RoyalAxe.Map;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkMovingSystem : IExecuteSystem
    {
        //todo: возможно вьюшку нельзя передавать напрямую. возможно лучше использовать адаптер или что-то такое.
        private readonly LevelInfrastructureView _levelInfrastructure;

        private readonly ILevelAdapter _axeCoreMap;
        private readonly ILevelPositionCalculation _levelPositionCalculation;
        private readonly TileCoreMapSettings _settings;
        private readonly IGroup<UnitsEntity> _playerGroup;

        private bool _hasend;
        private Vector3 _endPosition;

        public ChunkMovingSystem(ILevelAdapter axeCoreMap,
                                 TileCoreMapSettings settings,
                                 UnitsContext unitsContext,
                                 ILevelPositionCalculation levelPositionCalculation,
                                 LevelInfrastructureView levelInfrastructure)
        {
            _axeCoreMap = axeCoreMap;
            _settings = settings;
            _levelPositionCalculation = levelPositionCalculation;
            _levelInfrastructure = levelInfrastructure;
            _endPosition = _levelInfrastructure.PlayerEndPoint.position;
            _playerGroup = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.UnitsView, UnitsMatcher.Player));
        }

        public void Execute()
        {
            var movingTransform = _axeCoreMap.MoveRoot;
            movingTransform.Translate(new Vector3(0, Time.deltaTime * _settings.ChunkSpeed * _levelPositionCalculation.SpeedFactor, 0));

            if(_playerGroup.count == 0) return;
            var player = _playerGroup.GetSingleEntity();
            if(player== null) return;
            if (player.unitsView.RootTransform.position.y >= _endPosition.y)
            {
                _axeCoreMap.HandleNextChunk(null);
                _endPosition = _levelInfrastructure.PlayerEndPoint.position;
            }
        }

    }
}
