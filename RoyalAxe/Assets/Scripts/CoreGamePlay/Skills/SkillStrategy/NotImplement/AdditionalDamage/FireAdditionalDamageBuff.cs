
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff {
    public class FireAdditionalDamagePower : AdditionalDamagePower<FireAdditionalDamageSkillSettings>
    {
        public FireAdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory) { }
    }
}