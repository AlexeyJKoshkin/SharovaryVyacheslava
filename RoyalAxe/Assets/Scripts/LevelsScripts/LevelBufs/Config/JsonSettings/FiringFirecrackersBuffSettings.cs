using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FiringFirecrackersBuffSettings: BaseLevelBuffSettings
    {
        public int Amount;
        public float Radius;
        public float Cooldown;
        public int PhysicalDamage;
        public FiringFirecrackersBuffSettings() : base(LevelBuffType.FiringFirecrackers) { }
    }
}