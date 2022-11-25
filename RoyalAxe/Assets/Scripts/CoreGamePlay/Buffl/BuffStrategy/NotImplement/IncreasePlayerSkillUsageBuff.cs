namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerSkillUsageBuff : AbstractBuffStrategy<IncreasePlayerSkillUsageBuffSettings>
    {
       
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;


        

        public override void DoBuffStrategyActivate()
        {
            var         skillEntity = Player.unitActiveSkill.SkillEntity;
            var         usages      = skillEntity.useCounterSkill;
            skillEntity.ReplaceUseCounterSkill(usages.CurrentValue + Settings.AddAxe, usages.MaxValue + Settings.AddAxe);
        }

        public IncreasePlayerSkillUsageBuff(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider)
        {
            _unitsContext = unitsContext;
        }
    }
}