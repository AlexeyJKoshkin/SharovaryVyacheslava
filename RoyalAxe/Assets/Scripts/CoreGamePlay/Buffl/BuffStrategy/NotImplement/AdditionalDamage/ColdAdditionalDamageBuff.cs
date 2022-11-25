using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff<ColdAdditionalDamageBuffSettings>
    {
        public ColdAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitAddDamageToSkillUtility unitDamageApplierFactory, UnitsContext unitsContext) : base(provider, unitDamageApplierFactory, unitsContext) { }
    }
}