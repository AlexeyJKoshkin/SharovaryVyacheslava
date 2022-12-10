using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas
{
    public interface ISkillFactory
    {
        SkillEntity CreateRangeSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity owner);
        SkillEntity CreateWeaponSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity unit);
    }

    public interface IBuffFactory
    {
        SkillEntity CreateElementalBuff(UnitsEntity attacker, SkillConfigDef.Damage damage);
        SkillEntity BuildFreezeUnitBuf(UnitsEntity caster,float settingsDecelerationPercent);
    }
}