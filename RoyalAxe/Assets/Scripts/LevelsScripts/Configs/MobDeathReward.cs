using System;
using Core.Parser;

namespace RoyalAxe.CoreLevel {
    [Serializable]
    public class MobDeathReward
    {
        [ColumnName("Gold")]
        public int Gold;
        [ColumnName("EXP_level")]
        public int Expa;
        [ColumnName("Gems")]
        public int Gems;
    }
}