using System;
using System.Collections.Generic;
using System.Linq;
using Core.Parser;
using Newtonsoft.Json;
using RoyalAxe.Units.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoyalAxe
{
    /*
     * Пачка урона одной сущности   (весь урон от оружия, бафов другое.)
     */
    [Serializable]
    public class InfluenceApplierComposite
    {
        private Dictionary<DamageType, IDamageValue> _singleDamage = new Dictionary<DamageType, IDamageValue>();
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
            _cashedDamage.Clear();

            bool isCrit = false; // todo разобраться с критом

            ApplySingleDamage(attacker, target, isCrit);
            TryHandPeriodicDamage(attacker, target);
            return _cashedDamage;
        }

        private void TryHandPeriodicDamage(UnitsEntity attacker, UnitsEntity target)
        {
            for (int i = 0; i < PeriodicDamage.Count; i++)
            {
                PeriodicDamage[i].Apply(attacker, target);
            }
        }

        private void ApplySingleDamage(UnitsEntity attacker, UnitsEntity target, bool isCrit)
        {
            bool triggerAnimation = false;
            foreach (var data in _singleDamage)
            {
                var damage = data.Value;
                var damageInfo = new SingleDamageInfo()
                {
                    DamageType = data.Key,
                    Value      = isCrit ? damage.Damage : damage.Damage + data.Value.CriticalDamage
                };
                var hitInfo = new HitDamageInfo
                {
                    HitValue   = _influenceCalculator.ApplySingleDamage(attacker, target, damageInfo),
                    IsCritical = isCrit,
                    DamageType = data.Key
                };


                if (!triggerAnimation && hitInfo.HitValue < 0)
                {
                    //анимация уроная должна дергаться если урон действительно вызывается
                    target.unitAnimationEntity.AnimationEntity.isHitTrigger = true;
                    triggerAnimation                                        = true;
                }

                hitInfo.IsCritical = isCrit;
                _cashedDamage.Add(hitInfo);
            }
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            Get(type).IncreaseDamage(settingsValue);
        }

        public void IncreaseCriticalDamage(DamageType type, float settingsValue)
        {
            Get(type).CriticalDamage += settingsValue;
        }

        public void AddDamageSpan(DamageType type, float minValue, float maxValue)
        {
            SpanSingleDamageValue spanSingleDamageValue = new SpanSingleDamageValue(minValue, maxValue);
            if (_singleDamage.TryGetValue(type, out var oldDamage))
            {
                spanSingleDamageValue.CriticalDamage = oldDamage.CriticalDamage; // критический урон оставляю старый если был
            }

            _singleDamage[type] = spanSingleDamageValue;
        }


        public float GetSingleValue(DamageType physical)
        {
            float result = 0;
            if (_singleDamage.TryGetValue(physical, out var operation))
                result = operation.Damage;
            return result;
        }

        IDamageValue Get(DamageType type)
        {
            IDamageValue damage;
            if (_singleDamage.TryGetValue(type, out damage))
                return damage;
            damage = new SimpleSingleDamageValue();
            _singleDamage.Add(type, damage);
            return damage;
        }
    }
}