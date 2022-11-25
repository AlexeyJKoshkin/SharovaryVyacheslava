using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff {
    [Serializable]
    public class IncreasePlayerSkillUsageBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Optional_weapons")]
        public int AddAxe;
        
        public IncreasePlayerSkillUsageBuffSettings() : base(LevelBuffType.IncreaseSkillUsage) { }
    }
}