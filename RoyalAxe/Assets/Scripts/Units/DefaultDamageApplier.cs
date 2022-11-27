using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameKit;
using RoyalAxe.CharacterStat;

namespace RoyalAxe
{

    public interface IInfluenceApplierComposite : IInfluenceApplier
    {
        void IncreaseDamage(DamageType physical, float settingsValue);
    }

    public class InfluenceApplierComposite : IInfluenceApplierComposite, IEnumerable<IInfluenceApplier>
    {
        public readonly List<ISimpleInfluenceApplier> SingleDamage = new List<ISimpleInfluenceApplier>();
        public readonly List<IPeriodicInfluenceApplier> PeriodicDamage = new List<IPeriodicInfluenceApplier>();
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;

        public InfluenceApplierComposite(IUnitDamageApplierFactory unitDamageApplierFactory)
        {
            
            _unitDamageApplierFactory = unitDamageApplierFactory;
        }

        public void Apply(UnitsEntity attacker, UnitsEntity target)
        {
            this.ForEach(e=> e.Apply(attacker, target));
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            var physDamage = SingleDamage.FirstOrDefault(o => o.Type == type); // ищем
            if (physDamage != null) // есть основной урон.
            {
                physDamage.AddDamage(settingsValue); // увеличи урон
            }
            else
            {
                SingleDamage.Add(_unitDamageApplierFactory.CreateOneMomentDamage(type,settingsValue));
            }
        }
        
        public void AddPeriodicDamage(PeriodicDamageInfluenceData periodicDamageInfluenceData)
        {
            if(periodicDamageInfluenceData == null) return;
            PeriodicDamage.Add(_unitDamageApplierFactory.CreatePeriodicDamage(periodicDamageInfluenceData));
        }

        public IEnumerator<IInfluenceApplier> GetEnumerator()
        {
            for (int i = 0; i < SingleDamage.Count; i++)
            {
                yield return SingleDamage[i];
            }
            
            for (int i = 0; i < PeriodicDamage.Count; i++)
            {
                yield return PeriodicDamage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

      
    }

    public interface IInfluenceApplier
    {
        void Apply(UnitsEntity attacker, UnitsEntity target);
    }

    public interface ISimpleInfluenceApplier : IInfluenceApplier
    {
        DamageType Type { get; }
        float Value { get; }
        void AddDamage(float damageValue);
    }

    public interface IPeriodicInfluenceApplier : IInfluenceApplier
    {
    }

    public interface IDeBuffApplier : IInfluenceApplier
    {
        
    }
}