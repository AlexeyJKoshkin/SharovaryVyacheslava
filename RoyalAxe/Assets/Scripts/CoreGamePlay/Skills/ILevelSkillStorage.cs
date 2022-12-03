using System.Collections.Generic;

namespace RoyalAxe.LevelBuff {
    public interface ILevelSkillStorage : IReadOnlyCollection<ILevelSkill>
    {
        ILevelSkill Peek(LevelSkillType type);
    }
}