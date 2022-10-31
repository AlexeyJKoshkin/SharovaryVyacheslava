using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FiringBladeBuffSettings : BaseLevelBuffSettings
    {
        public int Damage;
        public float Cooldown;
        public FiringBladeBuffSettings() : base(LevelBuffType.FiringBlade) { }
    }
}