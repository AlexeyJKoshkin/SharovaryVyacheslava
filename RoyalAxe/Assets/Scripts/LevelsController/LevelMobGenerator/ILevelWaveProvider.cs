
namespace RoyalAxe.CoreLevel 
{
    public interface ILevelWaveProvider
    {
        float SpawnCooldown { get; }
        int MaxMobAmount { get;  }
        MobDeathReward CurrentMobReward { get;  }
        bool HasMob { get; }
        MobAtLevelData GenerateMobData();
    }

    public interface ILevelWaveLoader : ILevelWaveProvider
    {
        bool NextWave();
    }
}