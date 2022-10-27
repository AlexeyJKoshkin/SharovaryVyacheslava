using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class FireAdditionalDamageBuff : AdditionalDamageBuff
    {
        
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.FireAdditionDamageBuffSettings;
        }

        public FireAdditionalDamageBuff(UnitsContext unitsContext, IUnitDamageApplierFactory unitDamageApplierFactory, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, unitDamageApplierFactory, provider) { }
    }
}