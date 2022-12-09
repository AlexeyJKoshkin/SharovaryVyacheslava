using System.Collections.Generic;

namespace RoyalAxe.LevelSkill {
    public interface ILevelSkillStorage : IReadOnlyCollection<ILevelSkill>
    {
        ILevelSkill Peek(LevelSkillType type);
    }
}