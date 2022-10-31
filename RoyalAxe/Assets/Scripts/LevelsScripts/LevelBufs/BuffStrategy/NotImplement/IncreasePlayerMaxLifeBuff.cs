namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerMaxLifeBuff : AbstractBuffStrategy<IncreasePlayerMaxLifeBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerMaxLifeBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider):base(provider)
        {
            _unitsContext = unitsContext;
        }

        public override void DoBuffStrategyActivate()
        {
            Player.health.ChangeMaxValue(Settings.IncreaseValue).ApplyPermanentMod();
        }
    }
}