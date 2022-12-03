using System;

namespace RoyalAxe.LevelBuff {
    [Serializable]
    public abstract class BaseLevelSkillSettings
    {
        public LevelSkillType Type;

        public BaseLevelSkillSettings(LevelSkillType type)
        {
            Type = type;
        }
    }
}