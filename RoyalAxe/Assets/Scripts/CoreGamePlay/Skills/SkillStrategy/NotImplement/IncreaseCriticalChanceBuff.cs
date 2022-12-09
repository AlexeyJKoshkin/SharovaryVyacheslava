namespace RoyalAxe.LevelSkill
{
    public class IncreaseCriticalChancePlayerSkill : AbstractPlayerSkillStrategy<IncreaseCriticalChanceSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;


        public IncreaseCriticalChancePlayerSkill(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate() { }
        public override void DoLevelPowerDeActivate()
        {
            
        }
    }
}