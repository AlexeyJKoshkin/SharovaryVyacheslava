using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class HealPlayerLifeBuffSettings: BaseLevelBuffSettings
    {
        public float HealPercent;
        public HealPlayerLifeBuffSettings() : base(LevelBuffType.HealPlayerLife) { }
    }
}