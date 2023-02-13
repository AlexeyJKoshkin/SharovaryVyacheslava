using System.Collections.Generic;
using Entitas;
using RoyalAxe.EntitasSystems;
using RoyalAxe.Map;
using Sirenix.Utilities;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkMovingSystem : IExecuteSystem
    {
        private readonly IGroup<CoreGamePlayEntity> _movingChunks;

        private readonly ILevelAdapter _axeCoreMap;
        private readonly ILevelPositionCalculation _levelPositionCalculation;
        private readonly TileCoreMapSettings _settings;
        private IGroup<UnitsEntity> _movingMobs;

        public ChunkMovingSystem(CoreGamePlayContext coreGamePlayContext,
                                 ILevelAdapter axeCoreMap,
                                 TileCoreMapSettings settings,
                                 UnitsContext unitsContext,
                                 ILevelPositionCalculation levelPositionCalculation)
        {
            _axeCoreMap = axeCoreMap;
            _settings = settings;
            _levelPositionCalculation = levelPositionCalculation;

            _movingChunks =
                coreGamePlayContext.GetGroup(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.MovingChunk,
                                                                               CoreGamePlayComponentsLookup.ChunkView));
            _movingMobs = unitsContext.GetGroup(UnitsMatcherLibrary.MovingNavMeshUnits().Matcher());
        }

        public void Execute()
        {
            _movingMobs.AsEnumerable().ForEach(e => e.navMeshAgent.NavMeshAgent.updatePosition = false);
            var movingTransform = _axeCoreMap.ChunkRoot;
            movingTransform.Translate(new Vector3(0, Time.deltaTime * _settings.ChunkSpeed * _levelPositionCalculation.SpeedFactor, 0));
            var chunks = _movingChunks.GetEntities();
            _movingMobs.AsEnumerable().ForEach(e => e.navMeshAgent.NavMeshAgent.updatePosition = true);
            for (int i = 0; i < chunks.Length; i++)
            {
                MoveChunk(chunks[i]);
            }

            if (_levelPositionCalculation.CheckNeedRelocateToStartPoint(movingTransform.position.y))
            {
                List<Transform> childs = new List<Transform>(movingTransform.childCount);

                for (int i = 0; i < movingTransform.childCount; i++)
                {
                    childs.Add(movingTransform.GetChild(i));
                }

                movingTransform.DetachChildren();
                movingTransform.localPosition = new Vector3(0, 100, 0);
                childs.ForEach(e => e.SetParent(movingTransform, true));
            }
        }

        private void MoveChunk(CoreGamePlayEntity chunk)
        {
            chunk.ReplaceChunkBounds(chunk.chunkView.View.CalcChunkBounds());

            if (_levelPositionCalculation.IsFinishMoving(chunk)) _axeCoreMap.HandleNextChunk(chunk);
        }
    }
}
