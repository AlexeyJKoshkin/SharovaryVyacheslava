using System;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;

namespace Core.UserProfile
{
    public interface IInventoryProgress : IUserProgressProfile
    {
        IHeroEquipment Equipment { get; }
    }

    public interface IHeroEquipment
    {
        string EquippedWeaponId { get; }
    }

    [Serializable]
    public class ProfileInventoryProgress : BaseUserProgressData
    {
        public EquipItemsProgress EquipItemsProgress;
        public List<ItemInBagRecord> BagItems;
    }

    [Serializable]
    public class EquipItemsProgress : BaseUserProgressData
    {
        public string EquipWeaponId;
    }

    [Serializable]
    public class ItemInBagRecord
    {
        public string ItemId;
        public int Amount;
    }
}
