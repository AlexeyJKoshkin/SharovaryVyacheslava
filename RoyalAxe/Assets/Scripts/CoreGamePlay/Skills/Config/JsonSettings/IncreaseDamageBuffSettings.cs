using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class IncreaseDamageSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent")]
        public float Value;
        public IncreaseDamageSkillSettings() : base(LevelSkillType.IncreaseDamage) { }
    }
    
}