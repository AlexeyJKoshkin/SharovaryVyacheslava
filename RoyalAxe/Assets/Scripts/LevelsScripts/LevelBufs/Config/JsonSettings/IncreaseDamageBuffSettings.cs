using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreaseDamageBuffSettings: BaseLevelBuffSettings
    {
        public float Value;
        public IncreaseDamageBuffSettings() : base(LevelBuffType.IncreaseDamage) { }
    }
}