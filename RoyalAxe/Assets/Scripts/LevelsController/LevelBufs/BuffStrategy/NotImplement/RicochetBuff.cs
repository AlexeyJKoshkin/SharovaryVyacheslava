namespace RoyalAxe.LevelBuff
{
    public class RicochetBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;

        private readonly RicochetBuffSettings _settings;
        
        public RicochetBuff(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.SettingsComposite.RicochetBuffSetting;
        }
    }
}