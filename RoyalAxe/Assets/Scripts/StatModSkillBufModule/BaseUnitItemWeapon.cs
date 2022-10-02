using System.Collections.Generic;
using FluentBehaviourTree;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public abstract class BaseUnitItemWeapon : IEquipItem, IModificatorProvider
    {
        public UnitsEntity Owner => _equipable.Target;
        public SlotType AvailableSlot => SlotType.MainWeapon;
        private readonly EquipableSetterHelper _equipable;

        public BaseUnitItemWeapon()
        {
            _equipable = new EquipableSetterHelper(this);
        }

        IEnumerable<ICharacterStatModificator> IModificatorProvider.ApplyTempStats()
        {
            return GetTemStats();
        }

        IEnumerable<ICharacterStatModificator> IModificatorProvider.ApplyPermanentStatMods()
        {
            return ApplyPermanentStatMods();
        }

        protected virtual IEnumerable<ICharacterStatModificator> GetTemStats()
        {
            yield break;
        }

        protected virtual IEnumerable<ICharacterStatModificator> ApplyPermanentStatMods()
        {
            yield break;
        }

        public abstract BehaviourTreeStatus Execute(TimeData time);

        public abstract string NodeName { get; }

        public virtual void ApplyTo(UnitsEntity owner)
        {
            _equipable.ApplyTo(owner);
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            _equipable.RemoveFrom(owner);
        }
    }
}