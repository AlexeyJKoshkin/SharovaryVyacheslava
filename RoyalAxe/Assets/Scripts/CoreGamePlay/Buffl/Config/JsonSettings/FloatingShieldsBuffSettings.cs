using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FloatingShieldsBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Quantity_shields")]
        public int ShieldsCount;
        public int AbsorbedDamage;
        [ColumnName("Speed_rotation")]
        public float Speed;
        public FloatingShieldsBuffSettings() : base(LevelBuffType.FloatingShields) { }
    }
}