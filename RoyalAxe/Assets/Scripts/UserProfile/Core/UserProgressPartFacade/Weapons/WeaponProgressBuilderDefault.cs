using System.Collections.Generic;

namespace Core.UserProfile
{
    public class WeaponProgressBuilderDefault : IDefaultProgressFactory<UserAllWeaponsProgress>
    {
        public UserAllWeaponsProgress CreateDefault()
        {
            var current = new WeaponProgressData()
            {
                Weapon = new SaveEntityRecord(){Id = "Weapon_Player_default", Level = 1}
            };

            return new UserAllWeaponsProgress()
            {
                WeaponProgressData = new List<WeaponProgressData>(){current},
    };
        }
    }
}
