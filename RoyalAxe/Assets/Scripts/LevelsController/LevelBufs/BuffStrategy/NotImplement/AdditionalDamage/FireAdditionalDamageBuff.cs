namespace RoyalAxe.LevelBuff {
    public class FireAdditionalDamageBuff : AdditionalDamageBuff
    {
        public FireAdditionalDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, provider) { }
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.FireAdditionDamageBuffSettings;
        }
    }
}