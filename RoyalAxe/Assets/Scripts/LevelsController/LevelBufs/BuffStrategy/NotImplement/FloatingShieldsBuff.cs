namespace RoyalAxe.LevelBuff
{
    public class FloatingShieldsBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;
        private readonly FloatingShieldsBuffSettings _settings;
        public FloatingShieldsBuff(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.SettingsComposite.FloatingShieldsBuffSetting;
        }
    }
}