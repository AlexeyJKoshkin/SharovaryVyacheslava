
using System.Collections.Generic;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel 
{
    public interface ILevelWaveProvider
    {
        float SpawnCooldown { get; }
        int MaxMobAmount { get;  }
        MobDeathReward CurrentMobReward { get;  }
        bool HasMob { get; }
        LevelBuffType[] CurrentBuffs { get; }
        MobAtLevelData GenerateMobData();
    }

    public interface ILevelWaveLoader : ILevelWaveProvider
    {
        bool NextWave();
        void InitWaves(IReadOnlyList<LevelGeneratorSettings> infrastructurePackLevels);
    }
}