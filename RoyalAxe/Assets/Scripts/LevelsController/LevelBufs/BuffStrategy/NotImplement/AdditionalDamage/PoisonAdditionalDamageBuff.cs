namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamageBuff : AdditionalDamageBuff
    {
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.PoisonAdditionDamageBuffSettings;
        }

        public PoisonAdditionalDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, provider) { }
    }
}