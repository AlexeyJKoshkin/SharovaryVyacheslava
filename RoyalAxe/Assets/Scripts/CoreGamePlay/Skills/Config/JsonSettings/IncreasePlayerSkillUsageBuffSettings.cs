using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill {
    [Serializable]
    public class IncreasePlayerSkillUsageSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Optional_weapons")]
        public int AddAxe;
        
        public IncreasePlayerSkillUsageSkillSettings() : base(LevelSkillType.IncreaseSkillUsage) { }
    }
}