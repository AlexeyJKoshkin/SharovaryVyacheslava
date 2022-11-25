using System;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FiringBladeBuffSettings : BaseLevelBuffSettings
    {
        public SkillConfigDef.Damage Damage;

        [ColumnName("Shot_distance")]
        public float Shot_distance;
        public FiringBladeBuffSettings() : base(LevelBuffType.FiringBlade) { }
    }
}