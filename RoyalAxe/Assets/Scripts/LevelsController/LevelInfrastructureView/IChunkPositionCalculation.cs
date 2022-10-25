using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public interface IChunkPositionCalculation
    {
        float CalcStartChunkPos(IBound chunkBounds);
        float CalcNextChunkPos(IBound bearingChunkBounds, IBound bounds);
        float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds);
        bool CheckNeedRelocateToStartPoint(float positionY);
        int SpeedFactor { get; }
        bool IsFinishMoving(CoreGamePlayEntity chunk);
        float GetMobYPos(int mobAmount);

        Vector2 CalcWizardPosition(IBound wizardBounds);
    }
}
