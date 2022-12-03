using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreasePlayerSkillSpeedSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent")]
        public float Value;
        
        public IncreasePlayerSkillSpeedSkillSettings() : base(LevelSkillType.IncreasePlayerSkillSpeed) { }
    }
}