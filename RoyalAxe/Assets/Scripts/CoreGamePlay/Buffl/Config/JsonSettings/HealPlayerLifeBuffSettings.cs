using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class HealPlayerLifeBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Percent_HP")]
        public float HealPercent;
        public HealPlayerLifeBuffSettings() : base(LevelBuffType.HealPlayerLife) { }
    }
}