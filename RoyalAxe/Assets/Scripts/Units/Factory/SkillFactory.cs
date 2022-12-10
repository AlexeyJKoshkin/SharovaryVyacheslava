using System.Collections.Generic;
using RoyalAxe.Units.Stats;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.GameEntitas
{
    public class SkillFactory : AbstractEntityFactory<SkillEntity, SkillContext>, ISkillFactory
    {
        private readonly ITimerFactory _timerFactory;

        public SkillFactory(SkillContext skillContext,
                            ITimerFactory timerFactory) : base(skillContext)
        {
            _timerFactory = timerFactory;
        }


        public SkillEntity CreateRangeSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity owner)
        {
            var skill = CreateWeaponSkill(rangeParams, owner);
            var timer = _timerFactory.CreateRestoreUsageSkillTimer(skill, rangeParams.RangeCooldownAttack);
            skill.AddRestoreAttemptsTimer(1, timer);
            return skill;
        }

        public SkillEntity CreateWeaponSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity unit)
        {
            var skill = Context.CreateEntity();
            skill.AddUseCounterSkill(rangeParams.StartUsage, rangeParams.StartUsage);
            skill.AddPriceUseSkill(rangeParams.PriceUsage);
            unit.AddUnitActiveSkill(skill);
            return skill;
        }
    }
}