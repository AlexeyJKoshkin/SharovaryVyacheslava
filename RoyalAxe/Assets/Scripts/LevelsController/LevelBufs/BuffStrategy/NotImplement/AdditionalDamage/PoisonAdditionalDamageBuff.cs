using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamageBuff : AdditionalDamageBuff
    {
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.PoisonAdditionDamageBuffSettings;
        }

        public PoisonAdditionalDamageBuff(UnitsContext unitsContext, IUnitDamageApplierFactory unitDamageApplierFactory, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, unitDamageApplierFactory, provider) { }
    }
}