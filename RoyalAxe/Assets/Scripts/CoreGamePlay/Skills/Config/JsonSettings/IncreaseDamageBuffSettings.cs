using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreaseDamageSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent")]
        public float Value;
        public IncreaseDamageSkillSettings() : base(LevelSkillType.IncreaseDamage) { }
    }
    
}