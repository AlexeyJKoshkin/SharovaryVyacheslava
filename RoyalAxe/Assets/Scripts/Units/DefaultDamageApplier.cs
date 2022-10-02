using RoyalAxe.CharacterStat;

namespace RoyalAxe
{
    public interface IDamageApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target, IUnitsInfluenceCalculator unitsInfluenceCalculator);
    }

    public class DefaultDamageApplier
    {
        private readonly IUnitsInfluenceCalculator _influenceCalculator;

        public DefaultDamageApplier(IUnitsInfluenceCalculator influenceCalculator)
        {
            _influenceCalculator = influenceCalculator;
        }

        public void Attack(UnitsEntity attacker, UnitsEntity target)
        {
            /*foreach (var applier in attacker.damage.DamageAppliers)
            {
                applier.Apply(attacker, target);
            }*/

            /*foreach (var info in attacker.damage)
            {
                DoElementalDamage(attacker, target, info.OneMomentDamage);
               // info.PeriodicDamage?.ElementalDamage.TryApplyTo(attacker, target);
            }*/
        }

        private void DoElementalDamage(UnitsEntity attacker, UnitsEntity target, DamageInfluenceData mobDamage)
        {
            var calculator = _influenceCalculator.GetBy(mobDamage.ElementalDamageType);
            var damage     = calculator.PowerDamage(attacker, mobDamage.Damage);
            var damageInfo = calculator.ApplyTo(target, damage);
            HandleDamage(attacker, target, damageInfo);
        }

        private void HandleDamage(UnitsEntity attacker, UnitsEntity target, HitDamageInfo hitDamageInfo)
        {
            attacker.unitAnimationEntity.AnimationEntity.isAttackTrigger = true;
            target.ReplaceHitUnit(hitDamageInfo);
            target.unitAnimationEntity.AnimationEntity.isHitTrigger = true;
        }
    }
}