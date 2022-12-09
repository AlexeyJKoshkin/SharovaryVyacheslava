using Core.UserProfile;
using RoyalAxe.Configs;

namespace RoyalAxe.GameEntitas {
    public class UnitBlueprint : BaseBlueprint
    {
        public StatsConfig Stats;
        public SkillBlueprint ActiveSkill;

        public UnitBlueprint(SaveEntityRecord saveRecord):this(saveRecord.Id, saveRecord.Level)
        {
        }

        public UnitBlueprint(string id, int level) : base(id, level)
        {
        }
    }
}