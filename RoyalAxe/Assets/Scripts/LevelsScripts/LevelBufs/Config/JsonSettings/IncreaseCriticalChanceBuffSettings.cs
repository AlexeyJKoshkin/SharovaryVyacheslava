using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreaseCriticalChanceBuffSettings: BaseLevelBuffSettings
    {
        public float Value;
        public IncreaseCriticalChanceBuffSettings() : base(LevelBuffType.IncreaseCriticalChance) { }
    }
}