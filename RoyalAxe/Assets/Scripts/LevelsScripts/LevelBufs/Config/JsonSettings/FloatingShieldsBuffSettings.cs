using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FloatingShieldsBuffSettings: BaseLevelBuffSettings
    {
        public int ShieldsCount;
        public int AbsorbedDamage;
        public float Speed;
        public FloatingShieldsBuffSettings() : base(LevelBuffType.FloatingShields) { }
    }
}