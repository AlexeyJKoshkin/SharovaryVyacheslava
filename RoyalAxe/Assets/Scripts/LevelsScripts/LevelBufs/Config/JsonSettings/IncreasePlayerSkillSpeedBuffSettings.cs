using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreasePlayerSkillSpeedBuffSettings: BaseLevelBuffSettings
    {
        public float Value;
        
        public IncreasePlayerSkillSpeedBuffSettings() : base(LevelBuffType.IncreasePlayerSkillSpeed) { }
    }
}