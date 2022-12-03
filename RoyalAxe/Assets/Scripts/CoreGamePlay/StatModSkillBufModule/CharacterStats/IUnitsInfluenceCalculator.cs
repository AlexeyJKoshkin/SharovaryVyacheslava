using System;

namespace RoyalAxe.CharacterStat
{
    public interface IUnitsInfluenceCalculator
    {
        IDamageApplyOperation Physic { get; }
        IDamageApplyOperation Fire { get; }
        IDamageApplyOperation Cold { get; }
        IDamageApplyOperation Poison { get; }
        IDamageApplyOperation Blood { get; }
        IDamageApplyOperation GetBy(DamageType damageType);
        HitDamageInfo ApplySingleDamage(UnitsEntity attacker, UnitsEntity target, SingleDamageInfo data);
        HitDamageInfo ApplyElementalTimingDamage(UnitsEntity target, SingleDamageInfo data);
    }

    public class UnitsInfluenceCalculator : IUnitsInfluenceCalculator
    {
        public IDamageApplyOperation Physic { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.PhysicalDamageStat);
        public IDamageApplyOperation Fire { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.FireDamageStat);
        public IDamageApplyOperation Cold { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.ColdDamageStat);
        public IDamageApplyOperation Poison { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.PoisonDamageStat);
        public IDamageApplyOperation Blood { get; private set; } = new UniversalDamageCalculation(UnitsComponentsLookup.BloodDamageStat);

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
        
        
        public HitDamageInfo ApplySingleDamage(UnitsEntity attacker, UnitsEntity target, SingleDamageInfo data)
        {
            var calculator = this.GetBy(data.DamageType);
            var damage     = calculator.PowerDamage(attacker, data.Value);
            return  calculator.ApplyTo(target,damage);
        }
        
        public HitDamageInfo ApplyElementalTimingDamage(UnitsEntity target, SingleDamageInfo data) // елементальный урон от времени не усиливаем
        {
            //получаем калькулятор расчета урона                    // просто применяем урон.
            // Урон не может быть усилен/быть критом. возможно только уменьшение урона
            var calculator = this.GetBy(data.DamageType);
            return calculator.ApplyTo(target,data.Value);
        }
    }
}
