namespace RoyalAxe.LevelBuff
{
    public class IncreasePlayerSkillSpeedBuff : AbstractBuffStrategy
    {
        private readonly IncreasePlayerSkillSpeedBuffSettings _settings;
        private UnitsEntity Player => _unitsContext.playerEntity;
        private readonly UnitsContext _unitsContext;
        
        public IncreasePlayerSkillSpeedBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider)
        {
            _unitsContext = unitsContext;
            _settings     = provider.SettingsComposite.IncreasePlayerSkillSpeedBuffSetting;
        }

        public override void Activate()
        {
        }
    }
}