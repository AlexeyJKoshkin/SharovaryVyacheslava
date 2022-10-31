using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class InfectedBloodBuffSettings: BaseLevelBuffSettings
    {
        public float Radius;
        public float PhysicalDamage;
        public float Cooldown;


        public InfectedBloodBuffSettings() : base(LevelBuffType.InfectedBlood) { }
    }
}