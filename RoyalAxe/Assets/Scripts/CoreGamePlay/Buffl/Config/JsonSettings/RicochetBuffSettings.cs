using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class RicochetBuffSettings: BaseLevelBuffSettings
    {
        public float Radius;
        public int EnemyAmount;
        public float PhysicalDamage;
        public RicochetBuffSettings() : base(LevelBuffType.Ricochet) { }
    }
}