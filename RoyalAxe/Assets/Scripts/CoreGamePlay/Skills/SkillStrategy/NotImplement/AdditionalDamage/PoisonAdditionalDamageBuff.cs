
using RoyalAxe.Units.Stats;

namespace RoyalAxe.LevelSkill 
{
    public class PoisonAdditionalDamagePlayerSkill : AdditionalDamagePlayerSkill<PoisonAdditionalDamageSkillSettings>
    {
        public PoisonAdditionalDamagePlayerSkill(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory) { }
    }
}