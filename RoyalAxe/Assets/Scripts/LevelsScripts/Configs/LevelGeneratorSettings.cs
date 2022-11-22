using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;
using Newtonsoft.Json;

namespace RoyalAxe.CoreLevel 
{
    [Serializable]
    public class LevelGeneratorSettings : IDataObject
    {
        [ColumnName("Max_spawn_unit")]
        public int MaxMobAmount;
        [ColumnName("Spawn_cooldown")]
        public float SpawnCooldown;
        [ColumnName("Biome")]
        public BiomeType Type;
        [ColumnName("Level")]
        public int LevelNumber;
        public MobDeathReward MobDeathReward = new MobDeathReward();
        public List<MobAtLevelData> MobsData = new List<MobAtLevelData>();
        public WaveDestiny Destiny = new WaveDestiny();
        [JsonIgnore]
        public string UniqueID => LevelNumber.ToString();
        public bool IsSafePoint;
    }


    public enum BiomeType
    {
        Forest = 0,
        Desert = 1,
    }
}