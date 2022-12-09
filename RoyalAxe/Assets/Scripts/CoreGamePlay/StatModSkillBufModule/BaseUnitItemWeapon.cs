using System.Collections.Generic;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public abstract class BaseUnitItemWeapon : IEquipItem, IModificatorProvider
    {
        public UnitsEntity Owner => _helper.Target;
        public SlotType AvailableSlot => SlotType.MainWeapon;
        private readonly EquipableSetterHelper _helper;

        public BaseUnitItemWeapon()
        {
            _helper = new EquipableSetterHelper(this);
        }

        IEnumerable<ICharacterStatModificator> IModificatorProvider.ApplyTempStats()
        {
            return GetTemStats();
        }

        public abstract void ApplyPermanentStatMods();



        protected abstract IEnumerable<ICharacterStatModificator> GetTemStats();


        public virtual void ApplyTo(UnitsEntity owner)
        {
            _helper.ApplyTo(owner);
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            _helper.RemoveFrom(owner);
        }
    }
}