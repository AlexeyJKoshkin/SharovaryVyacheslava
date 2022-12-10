
using RoyalAxe.Units.Stats;

namespace RoyalAxe.LevelSkill {
    public class FireAdditionalDamagePlayerSkill : AdditionalDamagePlayerSkill<FireAdditionalDamageSkillSettings>
    {
        public FireAdditionalDamagePlayerSkill(ILevelBuffSettingCompositeProvider provider,
                                         UnitsContext unitsContext,
                                         IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory) { }
    }
}