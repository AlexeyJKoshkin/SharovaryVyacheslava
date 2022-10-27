using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff
    {
        
        protected override AdditionDamageBuffSettings GetSettings(LevelBuffSettingsComposite providerSettingsComposite)
        {
            return providerSettingsComposite.ColdAdditionDamageBuffSettings;
        }

        public ColdAdditionalDamageBuff(UnitsContext unitsContext, IUnitDamageApplierFactory unitDamageApplierFactory, ILevelBuffSettingCompositeProvider provider) : base(unitsContext, unitDamageApplierFactory, provider) { }
    }
}