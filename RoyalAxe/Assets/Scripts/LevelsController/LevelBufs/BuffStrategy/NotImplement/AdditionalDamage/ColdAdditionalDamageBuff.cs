namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff
    {
        
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.ColdAdditionDamageBuffSettings;
        }

        public ColdAdditionalDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, provider) { }
    }
}