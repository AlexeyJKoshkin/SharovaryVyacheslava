using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public interface IChunkPositionCalculation
    {
        float CalcStartChunkPos(Bounds levelBounds, Bounds chunkBounds);
        float CalcNextChunkPos(Bounds bearingChunkBounds, Bounds bounds);
        float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds);
        bool CheckNeedRelocateToStartPoint(float positionY);
        int SpeedFactor { get; }
        bool IsFinishMoving(CoreGamePlayEntity chunk);
        float GetMobYPos(int mobAmount);
    }

    public class ChunkPositionCalculation : IChunkPositionCalculation
    {
        public int SpeedFactor { get; } = 1;

        private float _mobZeroSpawn;
        
        public bool IsFinishMoving(CoreGamePlayEntity chunk)
        {
            var chunkTransform = chunk.chunkView.RootTransform;
            return chunkTransform.position.y >= chunk.movingChunk.YEndPoint;
        }

        public ChunkPositionCalculation(LevelInfrastructureView levelChunkView)
        {
            _mobZeroSpawn            =  levelChunkView.Bounds.min.y - 2.3f; // когда мобы идут снизу надо отнимать больше. т.к. не учитывается высоты моба
        }

        public float GetMobYPos(int mobAmount)
        {
            return _mobZeroSpawn  -mobAmount * 2;
        }

        public float CalcStartChunkPos(Bounds viewBounds, Bounds chunkBounds)
        {
            var downPosition = viewBounds.max.y;
            var halfSize = chunkBounds.extents.y;
            return downPosition - halfSize;
        }

        public float CalcNextChunkPos(Bounds bearingChunkBounds, Bounds nextChunk)
        {
            var upPosition = bearingChunkBounds.min.y;
            var halfSize   = nextChunk.extents.y;
             upPosition -= halfSize;
             return upPosition;
        }

        public float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds)
        {
            return levelViewBounds.max.y +chunkBounds.extents.y;
        }

        public bool CheckNeedRelocateToStartPoint(float positionY)
        {
            return positionY < 1000;
        }
    }
}
