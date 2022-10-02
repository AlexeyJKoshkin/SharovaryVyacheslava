using System;

namespace RoyalAxe.CharacterStat
{
    public interface IUnitsInfluenceCalculator
    {
        IDamageApplyOperation Physic { get; }
        IDamageApplyOperation Fire { get; }
        IDamageApplyOperation Cold { get; }
        IDamageApplyOperation Poison { get; }
        IDamageApplyOperation GetBy(DamageType damageType);
    }

    public class UnitsInfluenceCalculator : IUnitsInfluenceCalculator
    {
        public IDamageApplyOperation Physic { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.PhysicalDamageStat);
        public IDamageApplyOperation Fire { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.FireDamageStat);
        public IDamageApplyOperation Cold { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.ColdDamageStat);
        public IDamageApplyOperation Poison { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.PoisonDamageStat);

        public IDamageApplyOperation GetBy(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Physical: return Physic;
                case DamageType.Fire:     return Fire;
                case DamageType.Cold:     return Cold;
                case DamageType.Poison:   return Poison;
                default:                  throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
            }
        }
    }
}
