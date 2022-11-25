using System;
using System.Collections.Generic;

namespace Core.UserProfile
{
    [Serializable]
    public class WeaponProgressData : SaveEntityRecord { }

    [Serializable]
    public class UserAllWeaponsProgress : BaseUserProgressData
    {
        public List<WeaponProgressData> WeaponProgressData = new List<WeaponProgressData>();
    }
}