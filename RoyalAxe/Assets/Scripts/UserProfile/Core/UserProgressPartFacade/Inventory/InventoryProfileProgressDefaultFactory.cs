using System.Collections.Generic;

namespace Core.UserProfile
{
    class InventoryProfileProgressDefaultFactory :  IDefaultProgressFactory<ProfileInventoryProgress>
    {
        public ProfileInventoryProgress CreateDefault()
        {
            return new ProfileInventoryProgress()
            {
                BagItems  = new List<ItemInBagRecord>(),
                EquipItemsProgress = new EquipItemsProgress()
                {
                    EquipWeaponId = "weapon_grey_axe_1"
                }
            };
        }
    }
}
