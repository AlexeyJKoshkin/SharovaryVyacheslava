using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamageBuff : AdditionalDamageBuff<PoisonAdditionalDamageBuffSettings>
    {
        public PoisonAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitAddDamageToSkillUtility unitDamageApplierFactory, UnitsContext unitsContext) : base(provider, unitDamageApplierFactory, unitsContext) { }
    }
}