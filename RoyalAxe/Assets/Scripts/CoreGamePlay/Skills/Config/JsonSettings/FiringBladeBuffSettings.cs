using System;
using Core.Parser;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class FiringBladeSkillSettings : BaseLevelSkillSettings
    {
        public SkillConfigDef.Damage Damage;

        [ColumnName("Shot_distance")]
        public float Shot_distance;
        public FiringBladeSkillSettings() : base(LevelSkillType.FiringBlade) { }
    }
}