using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe
{
    /// <summary>
    /// Пачка урона одной сущности   (весь урон от оружия, бафов другое.)
    /// </summary>
    public class InfluenceApplierComposite : IInfluenceApplierComposite
    {
        //public readonly List<DamageInfluenceData> SingleDamage = new List<DamageInfluenceData>(); // простой одномоментный урон

        private Dictionary<DamageType, float> _singleDamage = new Dictionary<DamageType, float>();
        public readonly List<IPeriodicInfluenceApplier> PeriodicDamage = new List<IPeriodicInfluenceApplier>();
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;

        private readonly IUnitsInfluenceCalculator _calculator;

        public InfluenceApplierComposite(IUnitDamageApplierFactory unitDamageApplierFactory,
                                         IUnitsInfluenceCalculator influenceOperation)
        {
            _unitDamageApplierFactory = unitDamageApplierFactory;
            _calculator = influenceOperation;
        }

        //где-то дергается метод - атакующий пиздит цель
        public void Apply(UnitsEntity attacker, UnitsEntity target)
        {
            bool triggerAnimation = false;
            
            foreach (var data in _singleDamage)
            {
                var calculator = _calculator.GetBy(data.Key);
                var damage     = calculator.PowerDamage(attacker, data.Value);
                var damageInfo = calculator.ApplyTo(target,damage);

                if (!triggerAnimation && damageInfo.HitValue > 0) 
                {
                    //анимация уроная должна дергаться если урон действительно вызывается
                    target.unitAnimationEntity.AnimationEntity.isHitTrigger = true;
                    triggerAnimation = true;
                }
            }

            for (int i = 0; i < PeriodicDamage.Count; i++)
            {
                PeriodicDamage[i].Apply(attacker, target);
            }
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            if (_singleDamage.ContainsKey(type))
            {
                _singleDamage[type] += settingsValue;
            }
            else
            {
                _singleDamage[type] = settingsValue;
            }
        }
        


        public void Upgrade(SkillConfigDef.Damage damage)
        {
            this.IncreaseDamage(DamageType.Physical, damage.PhysicalDamage);
                        
            
            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                IncreaseDamage(damage.ElementalDamageType,damage.ElementalDamage);
            }
            
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var timeDamage = _unitDamageApplierFactory.CreatePeriodicDamage(damage);
                PeriodicDamage.Add(timeDamage);
            }
        }

        public void Downgrade(SkillConfigDef.Damage damage)
        {
            this.IncreaseDamage(DamageType.Physical, -damage.PhysicalDamage);
            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                IncreaseDamage(damage.ElementalDamageType,-damage.ElementalDamage);
            }
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var applier = PeriodicDamage.FirstOrDefault(o => o.DamageData == damage);
                if (applier != null)
                    PeriodicDamage.Remove(applier);
            }
        }
    }
}
