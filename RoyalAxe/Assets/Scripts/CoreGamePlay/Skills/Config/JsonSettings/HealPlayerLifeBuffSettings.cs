using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class HealPlayerLifeSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent_HP")]
        public float HealPercent;
        public HealPlayerLifeSkillSettings() : base(LevelSkillType.HealPlayerLife) { }
    }
}