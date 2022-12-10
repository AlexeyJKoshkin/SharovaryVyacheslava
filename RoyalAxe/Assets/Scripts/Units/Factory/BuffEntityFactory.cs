using System.Collections.Generic;
using RoyalAxe.Units;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas
{
    public class BuffEntityFactory : AbstractEntityFactory<SkillEntity, SkillContext>, IBuffFactory
    {
        private readonly IUnitsInfluenceCalculator _influenceCalculator;

        public BuffEntityFactory(SkillContext context, IUnitsInfluenceCalculator influenceCalculator, ISkillFactory skillFactory) : base(context)
        {
            _influenceCalculator = influenceCalculator;
        }

        public SkillEntity CreateElementalBuff(UnitsEntity attacker, SkillConfigDef.Damage damage)
        {
            var buff                  = CreateBlank(attacker);
            var elementalBufBehaviour = new ElementalDamageNode(buff, _influenceCalculator, damage);
            buff.AddBuffBehaviour(elementalBufBehaviour);

            return buff;
        }

        public SkillEntity BuildFreezeUnitBuf(UnitsEntity caster, float settingsDecelerationPercent)
        {
            var buff = CreateBlank(caster);
            var statModificator = new StatModificatorApplier(new FreesBuffSpeedModificator(settingsDecelerationPercent)); // при нанесении бафа будет изменен стат.
            var list = new List<IUnitApplierItem> {statModificator};
            buff.AddBuffApplier(list);
            return buff;
        }

        SkillEntity CreateBlank(UnitsEntity caster)
        {
            var buff = Context.CreateEntity();
            buff.AddBuffOwner(caster.uniqueUnitGUID.Guid, caster);
            return buff;
        }
    }
}