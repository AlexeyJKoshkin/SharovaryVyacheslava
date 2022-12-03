
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class PoisonAdditionalDamagePower : AdditionalDamagePower<PoisonAdditionalDamageSkillSettings>
    {
        public PoisonAdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory) { }
    }
}