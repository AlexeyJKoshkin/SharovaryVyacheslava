using System;
using System.Collections.Generic;
using System.Linq;
using Core.Parser;
using Newtonsoft.Json;
using RoyalAxe.Units.Stats;
using UnityEngine;

namespace RoyalAxe
{
    [Serializable]
    /// <summary>
    /// Пачка урона одной сущности   (весь урон от оружия, бафов другое.)
    /// </summary>
    public class InfluenceApplierComposite
    {
        class DamageValue
        {
            public float Damage 
            {
                get => _defaultDamage;
                set => _defaultDamage = Math.Max(0, value);
            }
            public float CritDamage
            {
                get => _criticalDamage;
                set => _criticalDamage = Math.Max(0, value);
            }

            private float _criticalDamage, _defaultDamage;

            public override string ToString()
            {
                return $"{_defaultDamage} - {_criticalDamage}";
            }
        }
        
        private Dictionary<DamageType, DamageValue> _singleDamage = new Dictionary<DamageType, DamageValue>();
        private IUnitsInfluenceCalculator _influenceCalculator;
        public readonly List<IPeriodicInfluenceApplier> PeriodicDamage = new List<IPeriodicInfluenceApplier>();

        private readonly List<HitDamageInfo> _cashedDamage = new List<HitDamageInfo>();


        public InfluenceApplierComposite(IUnitsInfluenceCalculator singleDamageOperation)
        {
            _influenceCalculator = singleDamageOperation;
        }

        //где-то дергается метод - атакующий пиздит цель
        public IReadOnlyList<HitDamageInfo> Apply(UnitsEntity attacker, UnitsEntity target)
        {
            bool triggerAnimation = false;
            _cashedDamage.Clear();

            bool isCrit = false; // todo разобраться с критом

            foreach (var data in _singleDamage)
            {
                var damage = data.Value;
                var damageInfo = new SingleDamageInfo()
                {
                    DamageType = data.Key,
                    Value      = isCrit ? damage.Damage : damage.Damage +data.Value.CritDamage
                };
                var hitInfo = new HitDamageInfo
                {
                    HitValue = _influenceCalculator.ApplySingleDamage(attacker, target, damageInfo),
                    IsCritical = isCrit,
                    DamageType = data.Key
                };


                if (!triggerAnimation && hitInfo.HitValue > 0)
                {
                    //анимация уроная должна дергаться если урон действительно вызывается
                    target.unitAnimationEntity.AnimationEntity.isHitTrigger = true;
                    triggerAnimation                                        = true;
                }

                hitInfo.IsCritical = isCrit;
                _cashedDamage.Add(hitInfo);
            }

            for (int i = 0; i < PeriodicDamage.Count; i++)
            {
                PeriodicDamage[i].Apply(attacker, target);
            }

            return _cashedDamage;
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            Get(type).Damage += settingsValue;
        }
        
        public void IncreaseCriticalDamage(DamageType type, float settingsValue)
        {
            Get(type).CritDamage += settingsValue;
        }


        public float GetSingleValue(DamageType physical)
        {
            float result = 0;
            if (_singleDamage.TryGetValue(physical, out var operation))
                result = operation.Damage;
            return result;
        }

        DamageValue Get(DamageType type)
        {
            DamageValue damage;
            if (_singleDamage.TryGetValue(type, out damage))
                return damage;
            damage = new DamageValue();
            _singleDamage.Add(type, damage);
            return damage;
        }
    }
}