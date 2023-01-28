using Core;
using UnityEngine;

namespace RoyalAxe.CoreLevel {
    public class LevelPositionCalculation : ILevelPositionCalculation
    {
        private readonly LevelInfrastructureView _levelChunkView;
        public int SpeedFactor { get; } = -1;

        private readonly float _mobZeroYSpawn; // начальная координата для спавна мобов по y
        
        public bool IsFinishMoving(CoreGamePlayEntity chunk)
        {
            var chunkTransform = chunk.chunkView.RootTransform;
            return chunkTransform.position.y <= chunk.movingChunk.YEndPoint;
        }

        public LevelPositionCalculation(LevelInfrastructureView levelChunkView)
        {
            _levelChunkView = levelChunkView;
            _mobZeroYSpawn = levelChunkView.Bounds.max.y; //- 1.5f; // когда мобы идут снизу надо отнимать больше. т.к. не учитывается высоты моба
            
            HLogger.TempLog(_mobZeroYSpawn);
        }

        public float GetMobYPos(int mobAmount)
        {
            return _mobZeroYSpawn +mobAmount * 2;
        }

        public Vector2 CalcWizardPosition(IBound bound)
        {
            var y = GetAppearYFor(bound, _levelChunkView);
            return new Vector2(_levelChunkView.PlayerStartPoint.position.x, y);
        }

        public float CalcStartChunkPos(IBound chunkBounds)
        {
            var viewBounds   = _levelChunkView.Bounds;
            var downPosition = viewBounds.min.y;
            var halfSize     = chunkBounds.Bounds.extents.y;
            return downPosition + halfSize;;
        }

        public float CalcNextChunkPos(IBound bearingChunkBounds, IBound nextChunk)
        {
            return GetAppearYFor(nextChunk, bearingChunkBounds);
        }

        public float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds)
        {
            return levelViewBounds.min.y -chunkBounds.extents.y;
        }

        public bool CheckNeedRelocateToStartPoint(float positionY)
        {
            return positionY > 1000;
        }
        
        float GetAppearYFor(IBound bounds, IBound parent)
        {
            var upPosition = parent.Bounds.max.y;
            var halfSize   = bounds.Bounds.extents.y;
            upPosition += halfSize;
            return upPosition;
        }
    }
}