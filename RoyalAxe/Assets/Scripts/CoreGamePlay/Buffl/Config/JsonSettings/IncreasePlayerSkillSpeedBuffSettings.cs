using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreasePlayerSkillSpeedBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Percent")]
        public float Value;
        
        public IncreasePlayerSkillSpeedBuffSettings() : base(LevelBuffType.IncreasePlayerSkillSpeed) { }
    }
}