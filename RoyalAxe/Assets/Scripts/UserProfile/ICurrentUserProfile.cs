using System;
using System.Collections.Generic;

namespace Core.UserProfile 
{
    public interface ICurrentUserProfile
    {
        HeroProgressData CurrentHeroData { get; }
        WeaponProgressData CurrentWeaponData { get; }
    }
    
    [Serializable]
    public class HeroProgressData
    {
        public string CharacterId = "Default_Hero";
        public byte Level = 1;
    }

    [Serializable]
    public class WeaponProgressData
    {
        public string WeaponID = "Weapon_Player_default";
        public byte Level = 1;
    }

    [Serializable]
    public class UserAllHeroesProgress
    {
        public string SelectedHeroId;
        public List<HeroProgressData> ProgressData = new List<HeroProgressData>();
    }
    
    [Serializable]
    public class UserAllWeaponsProgress
    {
        public string SelectedWeaponId;
        public List<WeaponProgressData> ProgressData = new List<WeaponProgressData>();
    }
}