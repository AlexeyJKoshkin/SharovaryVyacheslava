namespace RoyalAxe.LevelBuff
{
    public class FiringFirecrackersBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;
        private readonly FiringFirecrackersBuffSettings _settings;
        public FiringFirecrackersBuff(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.SettingsComposite.FiringFirecrackersBuffSetting;
        }
    }
}