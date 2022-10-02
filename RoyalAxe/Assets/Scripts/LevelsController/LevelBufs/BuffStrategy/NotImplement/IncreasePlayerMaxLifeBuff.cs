namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerMaxLifeBuff : AbstractBuffStrategy
    {
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly IncreasePlayerMaxLifeBuffSettings _settings;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerMaxLifeBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _settings     = provider.SettingsComposite.IncreasePlayerMaxLifeBuffSetting;
        }

        public override void Activate()
        {
            Player.health.ChangeMaxValue(_settings.IncreaseValue).ApplyPermanentMod();
        }
    }
}