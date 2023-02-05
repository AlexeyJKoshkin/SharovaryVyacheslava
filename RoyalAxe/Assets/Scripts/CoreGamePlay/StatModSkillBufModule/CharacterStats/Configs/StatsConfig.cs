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
        
        [ColumnName("Speed_move"), JsonProperty("ms")]
        public float MoveSpeed = 1;
    }
}