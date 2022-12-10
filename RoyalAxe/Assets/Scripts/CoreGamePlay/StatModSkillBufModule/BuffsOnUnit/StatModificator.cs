using System.Collections.Generic;
using GameKit;
using RoyalAxe.Units.Stats;
using RoyalAxe.CoreGamePlay;
using RoyalAxe.Units;

namespace RoyalAxe.Units.Stats 
{
    class StatModificatorApplier : IUnitApplierItem
    {
        private readonly IModificatorProvider[] _modificatorProvider;
        private readonly HashSet<ICharacterStatModificator> _usedMods = new HashSet<ICharacterStatModificator>();
        
        public StatModificatorApplier(params IModificatorProvider[] modificatorProvider)
        {
            _modificatorProvider = modificatorProvider;
        }

        public void ApplyTo(UnitsEntity owner)
        {
            for (int i = 0; i < _modificatorProvider.Length; i++)
            {
                var provider = _modificatorProvider[i];
                ApplyTo(provider, owner);
            }
        }

        private void ApplyTo(IModificatorProvider provider, UnitsEntity owner)
        {
            provider.ApplyPermanentStatMods(owner);
            foreach (ICharacterStatModificator modificator in provider.ApplyTempStats(owner)) _usedMods.Add(modificator);
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            _usedMods.ForEach(e => e.RemoveMode());
            _usedMods.Clear();
        }
    }
}