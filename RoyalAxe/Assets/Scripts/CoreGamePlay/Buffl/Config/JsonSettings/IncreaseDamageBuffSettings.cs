using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreaseDamageBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Percent")]
        public float Value;
        public IncreaseDamageBuffSettings() : base(LevelBuffType.IncreaseDamage) { }
    }
    
}