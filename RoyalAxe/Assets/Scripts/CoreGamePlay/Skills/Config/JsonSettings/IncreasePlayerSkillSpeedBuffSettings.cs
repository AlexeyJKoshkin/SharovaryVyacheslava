using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class IncreasePlayerSkillSpeedSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent")]
        public float Value;
        
        public IncreasePlayerSkillSpeedSkillSettings() : base(LevelSkillType.IncreasePlayerSkillSpeed) { }
    }
}