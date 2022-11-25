using System.Collections.Generic;

namespace Core.UserProfile
{
    internal class InventoryProfileProgressDefaultFactory : BaseDefaultProgressFactory<ProfileInventoryProgress>
    {
        public override ProfileInventoryProgress CreateDefault()
        {
            return new ProfileInventoryProgress
            {
                BagItems = new List<ItemInBagRecord>(),
                EquipItemsProgress = new EquipItemsProgress
                {
                    EquipWeaponId = "weapon_grey_axe_1"
                }
            };
        }
    }
}