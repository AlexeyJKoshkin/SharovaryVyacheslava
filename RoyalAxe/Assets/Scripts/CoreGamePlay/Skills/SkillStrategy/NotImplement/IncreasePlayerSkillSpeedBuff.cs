namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerSkillSpeedPower : AbstractPowerStrategyStrategy<IncreasePlayerSkillSpeedSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerSkillSpeedPower(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
        }

        public override void DoLevelPowerDeActivate()
        {
            
        }
    }
}