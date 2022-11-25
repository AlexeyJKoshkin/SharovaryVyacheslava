using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreaseCriticalChanceBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Up_percent_critical_damage")]
        public float Value;
        public IncreaseCriticalChanceBuffSettings() : base(LevelBuffType.IncreaseCriticalChance) { }
    }
}