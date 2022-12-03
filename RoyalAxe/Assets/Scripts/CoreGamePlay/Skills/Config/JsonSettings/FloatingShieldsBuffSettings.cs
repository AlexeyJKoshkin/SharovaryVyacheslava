using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
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