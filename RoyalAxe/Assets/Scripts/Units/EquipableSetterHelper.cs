using System.Collections.Generic;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CharacterStat
{
    public class EquipableSetterHelper
    {
        private readonly IModificatorProvider _modificatorProvider;
        public UnitsEntity Target { get; private set; }

        private HashSet<ICharacterStatModificator> _usedMods = new HashSet<ICharacterStatModificator>();

        public EquipableSetterHelper(IModificatorProvider modificatorProvider)
        {
            _modificatorProvider = modificatorProvider;
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            if (Target != owner)
            {
                Debug.LogError($"Current target {Target.creationIndex} but remove from {owner.creationIndex}");
                return;
            }

            _usedMods.ForEach(e => e.RemoveMode());
            _usedMods.Clear();
            Target = null;
        }

        public void ApplyTo(UnitsEntity owner)
        {
            if (Target != null)
            {
                Debug.LogError($"Twice apply buf {Target.creationIndex}");
                return;
            }

            Target = owner;
            _modificatorProvider.ApplyPermanentStatMods();
            foreach (ICharacterStatModificator modificator in _modificatorProvider.ApplyTempStats()) _usedMods.Add(modificator);
        }
    }
}