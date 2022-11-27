
using System.Collections.Generic;
using RoyalAxe.GameEntitas;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel 
{
    public interface ILevelWaveProvider
    {
    
        bool HasWave { get; }
    }

    public interface ILevelWaveLoader : ILevelWaveProvider
    {
        bool NextWave();
        void Init(ICoreLevelDataInfrastructure levelData);
    }
}