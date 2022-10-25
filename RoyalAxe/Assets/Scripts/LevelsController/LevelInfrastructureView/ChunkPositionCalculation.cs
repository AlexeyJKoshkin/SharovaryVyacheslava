using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkPositionCalculation : IChunkPositionCalculation
    {
        private readonly LevelInfrastructureView _levelChunkView;
        public int SpeedFactor { get; } = 1;

        private readonly float _mobZeroYSpawn; // начальная координата для спавна мобов по y
        
        public bool IsFinishMoving(CoreGamePlayEntity chunk)
        {
            var chunkTransform = chunk.chunkView.RootTransform;
            return chunkTransform.position.y >= chunk.movingChunk.YEndPoint;
        }

        public ChunkPositionCalculation(LevelInfrastructureView levelChunkView)
        {
            _levelChunkView = levelChunkView;
            _mobZeroYSpawn            =  levelChunkView.Bounds.min.y - 2.3f; // когда мобы идут снизу надо отнимать больше. т.к. не учитывается высоты моба
        }

        public float GetMobYPos(int mobAmount)
        {
            return _mobZeroYSpawn  -mobAmount * 2;
        }

        public Vector2 CalcWizardPosition(IBound bound)
        {
            var y = GetAppearYFor(bound, _levelChunkView);
            return new Vector2(_levelChunkView.PlayerStartPoint.position.x, y);
        }

        public float CalcStartChunkPos(IBound chunkBounds)
        {
            var viewBounds   = _levelChunkView.Bounds;
            var downPosition = viewBounds.max.y;
            var halfSize     = chunkBounds.Bounds.extents.y;
            return downPosition - halfSize;;
        }

        public float CalcNextChunkPos(IBound bearingChunkBounds, IBound nextChunk)
        {
            return GetAppearYFor(nextChunk, bearingChunkBounds);
        }

        public float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds)
        {
            return levelViewBounds.max.y +chunkBounds.extents.y;
        }

        public bool CheckNeedRelocateToStartPoint(float positionY)
        {
            return positionY < 1000;
        }
        
        float GetAppearYFor(IBound bounds, IBound parent)
        {
            var upPosition = parent.Bounds.min.y;
            var halfSize   = bounds.Bounds.extents.y;
            upPosition -= halfSize;
            return upPosition;
        }
    }
}
