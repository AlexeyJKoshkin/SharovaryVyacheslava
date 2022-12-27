using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;
using Newtonsoft.Json;

namespace RoyalAxe.Configs
{
    [Serializable]
    public class StatsConfig
    {
        [ColumnName("Health"), JsonProperty("he")]
        public float Health = 10;
        
        [ColumnName("Speed move"), JsonProperty("ms")]
        public float MoveSpeed = 1;
    }

    [Serializable]
    public class StatCollection : IDataObject
    {
        public string UniqueID { get; set; }

        public List<StatsConfig> Stats = new List<StatsConfig>();

        public StatsConfig GetByLevel(int lvl)
        {
            lvl--; // уровнь всегда на 1 больше чем индекс
            if (lvl < Stats.Count)
            {
                return Stats[lvl];
            }

            return new StatsConfig();
        }
    }
}