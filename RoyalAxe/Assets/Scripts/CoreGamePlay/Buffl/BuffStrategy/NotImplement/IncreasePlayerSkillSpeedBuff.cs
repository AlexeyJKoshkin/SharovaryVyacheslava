namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerSkillSpeedBuff : AbstractBuffStrategy<IncreasePlayerSkillSpeedBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerSkillSpeedBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoBuffStrategyActivate()
        {
        }
    }
}