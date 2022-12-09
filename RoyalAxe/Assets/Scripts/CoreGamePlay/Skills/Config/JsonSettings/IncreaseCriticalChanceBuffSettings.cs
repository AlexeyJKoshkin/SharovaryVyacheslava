using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class IncreaseCriticalChanceSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Up_percent_critical_damage")]
        public float Value;
        public IncreaseCriticalChanceSkillSettings() : base(LevelSkillType.IncreaseCriticalChance) { }
    }
}