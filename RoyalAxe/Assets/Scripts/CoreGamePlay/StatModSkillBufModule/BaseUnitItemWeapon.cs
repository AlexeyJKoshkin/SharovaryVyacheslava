using System.Collections.Generic;
using RoyalAxe.Units.Stats;
using RoyalAxe.CoreGamePlay;
using RoyalAxe.Units;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public abstract class BaseUnitItemWeapon : IEquipItem
    {
        public SlotType AvailableSlot => SlotType.MainWeapon;
        
        public UnitsEntity Owner { get; private set; }

        public readonly List<IUnitApplierItem> ApplyWorkers = new List<IUnitApplierItem>();

        public void ApplyTo(UnitsEntity target)
        {
            if (Owner != null)
            {
                Debug.LogError($"Twice apply buf {Owner.creationIndex}");
                return;
            }

            Owner = target;
            ApplyWorkers.ForEach(e=>e.RemoveFrom(target));
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            if (Owner != owner)
            {
                Debug.LogError($"Current target {Owner.creationIndex} but remove from {owner.creationIndex}");
                return;
            }
            ApplyWorkers.ForEach(e=>e.RemoveFrom(owner));
        }
        
      
     



    
    }
}