using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill
{
    [Serializable]
    public class FloatingShieldsSkillSettings: BaseLevelSkillSettings
    {
        [ColumnName("Quantity_shields")]
        public int ShieldsCount;
        public int AbsorbedDamage;
        [ColumnName("Speed_rotation")]
        public float Speed;
        public FloatingShieldsSkillSettings() : base(LevelSkillType.FloatingShields) { }
    }
}