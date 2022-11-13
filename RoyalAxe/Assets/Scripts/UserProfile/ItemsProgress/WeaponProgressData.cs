using System;
using System.Collections.Generic;

namespace Core.UserProfile {
    [Serializable]
    public class WeaponProgressData
    {
        public string WeaponID = "Weapon_Player_default";
        public int Level = 1;
    }
    
    [Serializable]
    public class UserAllWeaponsProgress
    {
        public string SelectedWeaponId;
        public List<WeaponProgressData> ProgressData = new List<WeaponProgressData>();
    }
}