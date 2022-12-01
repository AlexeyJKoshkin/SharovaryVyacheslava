namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerMaxLifePower : AbstractPowerStrategyStrategy<IncreasePlayerMaxLifeBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerMaxLifePower(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
            Player.health.ChangeMaxValue(Settings.IncreaseValue).ApplyPermanentMod();
        }

        public override void DoLevelPowerDeActivate()
        {
        }
    }
}