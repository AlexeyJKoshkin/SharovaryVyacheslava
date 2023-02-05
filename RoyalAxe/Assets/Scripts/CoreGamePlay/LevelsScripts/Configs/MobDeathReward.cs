using System;
using Core.Parser;
using Newtonsoft.Json;

namespace RoyalAxe.CoreLevel {
    [Serializable]
    public class MobDeathReward
    {
        [ColumnName("Gold"),JsonProperty("g") ]
        public int Gold;
        [ColumnName("EXP_level"),JsonProperty("e")]
        public int Expa;
    }
}