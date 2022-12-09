namespace RoyalAxe.LevelSkill
{
    public class IncreasePlayerSkillUsagePlayerSkill : AbstractPlayerSkillStrategy<IncreasePlayerSkillUsageSkillSettings>
    {
       
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;


        

        public override void DoLevelPowerActivate()
        {
            var         skillEntity = Player.unitActiveSkill.SkillEntity;
            var         usages      = skillEntity.useCounterSkill;
            skillEntity.ReplaceUseCounterSkill(usages.CurrentValue + Settings.AddAxe, usages.MaxValue + Settings.AddAxe);
        }

        public override void DoLevelPowerDeActivate()
        {
        }

        public IncreasePlayerSkillUsagePlayerSkill(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider)
        {
            _unitsContext = unitsContext;
        }
    }
}