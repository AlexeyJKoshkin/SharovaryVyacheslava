namespace RoyalAxe.CoreLevel 
{
    public interface ILevelWaveProvider
    {
        float SpawnCooldown { get; }
        int MaxMobAmount { get;  }
        MobDeathReward CurrentMobReward { get;  }
        bool HasMob { get; }
        bool NextWave();
        void LoadWave(int waveNumber);
        (string mobId, byte mobLevel) GenerateMobDataForSpawn();
    }
}