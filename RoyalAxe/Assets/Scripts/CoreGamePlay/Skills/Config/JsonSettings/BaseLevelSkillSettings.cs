using System;
using Core.Parser;

namespace RoyalAxe.LevelSkill {
    [Serializable]
    public abstract class BaseLevelSkillSettings
    {
        public LevelSkillType Type;
        [ColumnName("Localization")]
        public string Localization;

        public BaseLevelSkillSettings(LevelSkillType type)
        {
            Type = type;
        }
    }
}