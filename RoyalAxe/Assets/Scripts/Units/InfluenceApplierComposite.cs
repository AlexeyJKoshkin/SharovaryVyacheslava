using System;
using System.Collections.Generic;
using System.Linq;
using Core.Parser;
using Newtonsoft.Json;
using RoyalAxe.CharacterStat;

namespace RoyalAxe
{



    [Serializable]


    /// <summary>
    /// Пачка урона одной сущности   (весь урон от оружия, бафов другое.)
    /// </summary>
    public class InfluenceApplierComposite
    {
        private Dictionary<DamageType, float> _singleDamage = new Dictionary<DamageType, float>();
        private IUnitsInfluenceCalculator _influenceCalculator;
        public readonly List<IPeriodicInfluenceApplier> PeriodicDamage = new List<IPeriodicInfluenceApplier>();
       

        public InfluenceApplierComposite(IUnitsInfluenceCalculator singleDamageOperation)
        {
            _influenceCalculator = singleDamageOperation;
        }

        //где-то дергается метод - атакующий пиздит цель
        public void Apply(UnitsEntity attacker, UnitsEntity target)
        {
            bool triggerAnimation = false;
            
            foreach (var data in _singleDamage)
            {
                var damageInfo = _influenceCalculator.ApplySingleDamage(attacker, target,new SingleDamageInfo(){DamageType = data.Key, Value = data.Value});
                
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
        


        public float GetSingleValue(DamageType physical)
        {
            float result = 0;
            if (_singleDamage.TryGetValue(physical, out var operation))
                result = operation;
            return result;
        }
    }
}
