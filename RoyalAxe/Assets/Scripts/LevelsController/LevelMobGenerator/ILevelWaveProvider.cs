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
        CoreGamePlayEntity LoadWave(int waveNumber);
    }
}