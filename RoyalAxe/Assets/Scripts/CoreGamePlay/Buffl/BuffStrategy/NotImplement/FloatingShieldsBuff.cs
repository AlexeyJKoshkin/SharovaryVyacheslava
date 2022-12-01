namespace RoyalAxe.LevelBuff
{
    public class FloatingShieldsPower : AbstractPowerStrategyStrategy<FloatingShieldsBuffSettings>
    {
        public FloatingShieldsPower(ILevelBuffSettingCompositeProvider provider) : base(provider) { }
        public override void DoLevelPowerActivate()
        {
            
        }

        public override void DoLevelPowerDeActivate()
        {
            }
    }
}