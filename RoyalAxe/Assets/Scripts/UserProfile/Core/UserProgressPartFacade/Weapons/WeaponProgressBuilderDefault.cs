using System.Collections.Generic;

namespace Core.UserProfile
{
    public class WeaponProgressBuilderDefault : IDefaultProgressFactory<UserAllWeaponsProgress>
    {
        public UserAllWeaponsProgress CreateDefault()
        {
            var current = new WeaponProgressData();

            return new UserAllWeaponsProgress()
            {
                ProgressData = new List<WeaponProgressData>(){current},
    };
        }
    }
}
