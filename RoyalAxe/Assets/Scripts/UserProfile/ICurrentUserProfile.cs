using System;
using System.Collections.Generic;

namespace Core.UserProfile 
{
    public interface ICurrentUserProfile
    {
        HeroProgressData CurrentHeroData { get; }
        WeaponProgressData CurrentWeaponData { get; }
        LevelsProgress LastLevel { get; }
    }


}