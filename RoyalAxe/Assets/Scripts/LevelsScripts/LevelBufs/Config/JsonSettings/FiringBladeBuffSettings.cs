using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class FiringBladeBuffSettings : BaseLevelBuffSettings
    {
        public int Damage = 1;
        public float Cooldown = 100;
        public FiringBladeBuffSettings() : base(LevelBuffType.FiringBlade) { }
    }
}