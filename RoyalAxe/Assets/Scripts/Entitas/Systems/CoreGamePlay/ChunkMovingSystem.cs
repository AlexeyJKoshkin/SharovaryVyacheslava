using System.Collections.Generic;
using Entitas;
using RoyalAxe.Map;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkMovingSystem : IExecuteSystem
    {
        private readonly IGroup<CoreGamePlayEntity> _movingChunks;

        private readonly ILevelAdapter _axeCoreMap;
        private IChunkPositionCalculation _chunkPositionCalculation;
        private readonly TileCoreMapSettings _settings;

        public ChunkMovingSystem(CoreGamePlayContext coreGamePlayContext,
                                 ILevelAdapter axeCoreMap,
                                 TileCoreMapSettings settings,
                                 IChunkPositionCalculation chunkPositionCalculation)
        {
            _axeCoreMap = axeCoreMap;
            _settings = settings;
            _chunkPositionCalculation = chunkPositionCalculation;

            _movingChunks =
                coreGamePlayContext.GetGroup(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.MovingChunk,
                                                                               CoreGamePlayComponentsLookup.ChunkView));
        }

        public void Execute()
        {
            var movingTransform = _axeCoreMap.ChunkRoot;
            movingTransform.Translate(new Vector3(0, Time.deltaTime * _settings.ChunkSpeed * _chunkPositionCalculation.SpeedFactor, 0));
            var chunks = _movingChunks.GetEntities();

            for (int i = 0; i < chunks.Length; i++)
            {
                MoveChunk(chunks[i]);
            }

            if (_chunkPositionCalculation.CheckNeedRelocateToStartPoint(movingTransform.position.y))
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

            if (_chunkPositionCalculation.IsFinishMoving(chunk)) _axeCoreMap.HandleNextChunk(chunk);
        }
    }
}
