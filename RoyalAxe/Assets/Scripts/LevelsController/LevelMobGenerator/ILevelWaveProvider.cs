using System.Collections.Generic;

namespace RoyalAxe.CoreLevel 
{
    public interface ILevelWaveProvider
    {
        float SpawnCooldown { get; }
        int MaxMobAmount { get;  }
        MobDeathReward CurrentMobReward { get;  }
    
    }

    public interface ILevelWaveLoader : ILevelWaveProvider
    {
        bool NextWave();
        CoreGamePlayEntity InitWaves(IReadOnlyList<LevelGeneratorSettings> infrastructurePackLevels);
    }
}