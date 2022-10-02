namespace RoyalAxe.LevelBuff
{
    public class InfectedBloodBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;
        private readonly InfectedBloodBuffSettings _settings;
        
        public InfectedBloodBuff(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.SettingsComposite.InfectedBloodBuffSetting;
        }
    }
}