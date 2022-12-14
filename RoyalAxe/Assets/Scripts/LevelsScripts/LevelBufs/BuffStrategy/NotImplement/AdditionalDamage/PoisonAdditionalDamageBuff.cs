using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamageBuff : AdditionalDamageBuff<PoisonAdditionalDamageBuffSettings>
    {
        public PoisonAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitDamageApplierFactory, UnitsContext unitsContext) : base(provider, unitDamageApplierFactory, unitsContext) { }
    }
}