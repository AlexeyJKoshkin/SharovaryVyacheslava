using System;

namespace RoyalAxe.Units.Stats
{
    public interface IUnitsInfluenceCalculator
    {
        IDamageApplyOperation Physic { get; }
        IDamageApplyOperation Fire { get; }
        IDamageApplyOperation Cold { get; }
        IDamageApplyOperation Poison { get; }
        IDamageApplyOperation Blood { get; }
        IDamageApplyOperation GetBy(DamageType damageType);
        float ApplySingleDamage(UnitsEntity attacker, UnitsEntity target, SingleDamageInfo data);
        float ApplyElementalTimingDamage(UnitsEntity target, SingleDamageInfo data);
    }

    public class UnitsInfluenceCalculator : IUnitsInfluenceCalculator
    {
        public IDamageApplyOperation Physic { get; private set; } = new UniversalDamageCalculation()
        {
            PowerDamageOperation = new PowerDamageOperation(UnitsComponentsLookup.PhysicalDamageStat)
        };
        public IDamageApplyOperation Fire { get; private set; } = new UniversalDamageCalculation()
        {
            PowerDamageOperation = new PowerDamageOperation(UnitsComponentsLookup.FireDamageStat)
        };
        public IDamageApplyOperation Cold { get; private set; } = new UniversalDamageCalculation()
        {
            PowerDamageOperation = new PowerDamageOperation(UnitsComponentsLookup.ColdDamageStat)
        };
        public IDamageApplyOperation Poison { get; private set; } = new UniversalDamageCalculation()
        {
            PowerDamageOperation = new PowerDamageOperation(UnitsComponentsLookup.PoisonDamageStat)
        };
        public IDamageApplyOperation Blood { get; private set; } = new UniversalDamageCalculation()
        {
            PowerDamageOperation = new PowerDamageOperation(UnitsComponentsLookup.BloodDamageStat)
        };

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


        public float ApplySingleDamage(UnitsEntity attacker, UnitsEntity target, SingleDamageInfo data)
        {
            var calculator = this.GetBy(data.DamageType);
            return calculator.ApplyDamage(attacker, target, data.Value);
        }

        public float ApplyElementalTimingDamage(UnitsEntity target, SingleDamageInfo data) // елементальный урон от времени не усиливаем
        {
            //получаем калькулятор расчета урона                    // просто применяем урон.
            var calculator = this.GetBy(data.DamageType);
            return calculator.ApplyDamage(target, data.Value);
        }
    }
}