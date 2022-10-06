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
        public MobDeathReward MobDeathReward;
        public List<MobAtLevelData> MobsData = new List<MobAtLevelData>();
        [JsonIgnore]
        public string UniqueID => LevelNumber.ToString();
    }
    
    [Serializable]
    public class MobAtLevelData
    {
        public string MobId;
        public byte Level;
        public byte TotalAmount;

        public MobAtLevelData() { }

        public MobAtLevelData(MobAtLevelData old)
        {
            MobId = old.MobId;
            Level = old.Level;
            TotalAmount = old.TotalAmount;
        }
    }

    [Serializable]
    public struct MobDeathReward
    {
        [ColumnName("Gold")]
        public int Gold;
        [ColumnName("EXP_level")]
        public int Expa;
        [ColumnName("Gems")]
        public int Gems;
    }


    public enum BiomeType
    {
        Forest =0,
        Desert =1,
        
    }
}