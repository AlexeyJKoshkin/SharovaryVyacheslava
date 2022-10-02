namespace RoyalAxe.LevelBuff
{
    public class IncreaseCriticalChanceBuff : AbstractBuffStrategy
    {
        private readonly IncreaseCriticalChanceBuffSettings _settings;

        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;


        public IncreaseCriticalChanceBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _settings     = provider.SettingsComposite.IncreaseCriticalChanceBuffSetting;
        }

        public override void Activate() { }
    }
}