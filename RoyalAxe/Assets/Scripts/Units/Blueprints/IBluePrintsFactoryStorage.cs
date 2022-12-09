namespace RoyalAxe.CoreLevel {
    public interface IBluePrintsFactoryStorage
    {
        IUnitsBlueprintsFactory Units { get; }
        ISkillBlueprintsFactory Skill { get; }
    }

    public class BluePrintsFactoryStorage : IBluePrintsFactoryStorage
    {
        public BluePrintsFactoryStorage(IUnitsBlueprintsFactory units, ISkillBlueprintsFactory skill)
        {
            Units = units;
            Skill = skill;
        }
        public IUnitsBlueprintsFactory Units { get; }
        public ISkillBlueprintsFactory Skill { get; }
    }
}