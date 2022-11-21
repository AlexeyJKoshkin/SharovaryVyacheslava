#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Core.UserProfile
{
    public static class IUserProfileExtension
    {
        /*public static HeroProgressData LoadCurrentHero(this UserProfileData userProfileSave)
        {
            if (userProfileSave == null) return null;
            var hero = userProfileSave.HeroProgress;
            return DefaultFind(hero.SelectedHeroId, hero.ProgressData, (o => o.CharacterId == hero.SelectedHeroId));

        }
        
        public static WeaponProgressData LoadCurrentWeapon(this UserProfileData userProfileSave)
        {
            if (userProfileSave == null) return null;
            var weapons = userProfileSave.WeaponProgress;

            return DefaultFind(weapons.SelectedWeaponId, weapons.ProgressData, (o => o.WeaponID == weapons.SelectedWeaponId));

        }*/
      public  static T DefaultFind<T>(this List<T> list,string key, Predicate<T> predicate) where T : new()
        {
            if (string.IsNullOrEmpty(key))
            {
                if (list.Count > 0) return list[0] ?? new T();
            }
            return list.FirstOrDefault(o => predicate(o)) ?? new T();
        }
    }
}