using System.Collections.Generic;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public interface IUnitAddDamageToSkillUtility
    {
        void AddDamageToUnit(DamageComponent damage, SkillConfigDef.Damage damageData);
    }

    public class UnitAddDamageUtility : IUnitAddDamageToSkillUtility
    {
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;

        public UnitAddDamageUtility(IUnitDamageApplierFactory unitDamageApplierFactory)
        {
            _unitDamageApplierFactory = unitDamageApplierFactory;
        }

        public void AddDamageToUnit(DamageComponent damage, SkillConfigDef.Damage damageData)
        {
            var simplePhysDamage = _unitDamageApplierFactory.CreateOneMomentDamage(DamageType.Physical, damageData.PhysicalDamage);
            damage.SingleDamage.Add(simplePhysDamage);

            if (damageData.ElementalDamage > 0 && damageData.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                var simpleElementalDamage = _unitDamageApplierFactory.CreateOneMomentDamage(damageData.ElementalDamageType, damageData.ElementalDamage);
                damage.SingleDamage.Add(simpleElementalDamage);
            }

            if (damageData.ElementalDamage > 0 && damageData.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var periodicDamageInfluenceData = new PeriodicDamageInfluenceData
                {
                    MagicDuration       = damageData.MagicDuration,
                    DamageCooldown      = damageData.DamageCooldown,
                    Damage              = damageData.ElementalDamage,
                    ElementalDamageType = damageData.ElementalDamageType
                };

                var periodicDamage = _unitDamageApplierFactory.CreatePeriodicDamage(periodicDamageInfluenceData);
                damage.PeriodicDamage.Add(periodicDamage);
            }
        }
    }
}