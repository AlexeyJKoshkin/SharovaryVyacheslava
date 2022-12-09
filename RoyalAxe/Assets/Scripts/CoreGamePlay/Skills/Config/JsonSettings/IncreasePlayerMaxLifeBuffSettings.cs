using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class IncreasePlayerMaxLifeSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Percent")]
        //потом решим это будет процент или число
        public int IncreaseValue = 50;
        public IncreasePlayerMaxLifeSkillSettings() : base(LevelSkillType.IncreasePlayerMaxLife) { }
    }
}