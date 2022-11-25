using System.Collections.Generic;

namespace Core.UserProfile
{
    public class WeaponProgressBuilderDefault : IDefaultProgressFactory<UserAllWeaponsProgress>
    {
        public UserAllWeaponsProgress CreateDefault()
        {
            var current = new WeaponProgressData()
            {
                Id = "weapon_grey_axe_1", Level = 1
            };

            return new UserAllWeaponsProgress()
            {
                WeaponProgressData = new List<WeaponProgressData>(){current},
    };
        }
    }
}
