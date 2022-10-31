using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff<ColdAdditionalDamageBuffSettings>
    {
        public ColdAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitDamageApplierFactory, UnitsContext unitsContext) : base(provider, unitDamageApplierFactory, unitsContext) { }
    }
}