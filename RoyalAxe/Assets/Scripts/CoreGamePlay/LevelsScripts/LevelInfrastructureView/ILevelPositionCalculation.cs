using UnityEngine;

namespace RoyalAxe.CoreLevel {
    public interface ILevelPositionCalculation
    {
        float CalcStartChunkPos(IBound chunkBounds);
        float CalcNextChunkPos(IBound bearingChunkBounds, IBound bounds);
        float GetEndPoint(Bounds levelViewBounds, Bounds chunkBounds);
        bool CheckNeedRelocateToStartPoint(float positionY);
        int SpeedFactor { get; }
        float GetMobYPos(int mobAmount);

        Vector2 CalcWizardPosition(IBound wizardBounds);
    }
}