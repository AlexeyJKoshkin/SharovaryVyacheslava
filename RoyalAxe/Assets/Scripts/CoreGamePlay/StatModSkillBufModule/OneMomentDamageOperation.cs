using UnityEngine;

namespace RoyalAxe.CharacterStat
{
    public class OneMomentInfluenceOperation : ISimpleInfluenceApplier
    {
        public DamageType Type => _damage.ElementalDamageType;
        public float Value => _damage.Damage;
        private readonly DamageInfluenceData _damage;
        private readonly IUnitsInfluenceCalculator _calculator;
        
        public OneMomentInfluenceOperation(DamageInfluenceData damage, IUnitsInfluenceCalculator calculator)
        {
            _damage = damage;
            _calculator = calculator;
        }

        public void AddDamage(float damage)
        {
            _damage.Damage = Mathf.Max(0, _damage.Damage + damage);
        }

        public void Apply(UnitsEntity attacker, UnitsEntity target)
        {
            var calculator = _calculator.GetBy(_damage.ElementalDamageType);
            var damage = calculator.PowerDamage(attacker, _damage.Damage);
            var damageInfo = calculator.ApplyTo(target,damage);
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
