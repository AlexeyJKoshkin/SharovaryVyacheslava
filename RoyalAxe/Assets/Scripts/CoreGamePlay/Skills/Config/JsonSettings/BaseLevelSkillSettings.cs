using System;

namespace RoyalAxe.LevelSkill {
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