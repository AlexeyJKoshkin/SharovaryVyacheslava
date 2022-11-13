#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Core.UserProfile
{
    public static class IUserProfileExtension
    {
        public static HeroProgressData LoadCurrentHero(this UserProfileData userProfileSave)
        {
            if (userProfileSave == null) return null;
            var hero = userProfileSave.HeroProgress;
            if (string.IsNullOrEmpty(hero.SelectedHeroId)) return hero.ProgressData.FirstOrDefault();
            return hero.ProgressData.FirstOrDefault(o => o.CharacterId == hero.SelectedHeroId);
        }
        
        public static WeaponProgressData LoadCurrentWeapon(this UserProfileData userProfileSave)
        {
            if (userProfileSave == null) return null;
            var weapons = userProfileSave.WeaponProgress;
            
            return DefaultFind(weapons.SelectedWeaponId, weapons.ProgressData)
                ?? weapons.ProgressData.FirstOrDefault(o => o.WeaponID == weapons.SelectedWeaponId);
        }
        
        static T DefaultFind<T>(string key, List<T> list, Predicate<T> predicate)
        {
            return string.IsNullOrEmpty(key) ? list.FirstOrDefault();
            return default;
        }
        

        static T DefaultFind<T>(string key, List<T> list)
        {
            if (string.IsNullOrEmpty(key)) return list.FirstOrDefault();
            return default;
        }
    }

    /*public interface IUserProfileSave
    {
        string Name { get; }
        UserProfileData UserProfileData { get; }
    }*/

    /*public class UserProfileSave 
    {
        private readonly IUserProfileBuilder<UserProfileData> _builder;
        private DirectoryInfo FolderInfo => UserProfileData.FolderPath;

        public UserProfileSave(UserProfileData userProfileData, IUserProfileBuilder<UserProfileData> builder)
        {
            UserProfileData = userProfileData;
            _builder        = builder;
            if (!FolderInfo.Exists)
            {
                FolderInfo.Create();
            }
            else
            {
                _builder.BuildFrom(UserProfileData, FolderInfo.FullName);
            }
        }

      

        public string Name => FolderInfo.Name;
        public UserProfileData UserProfileData { get; }

        public void Save()
        {
            _builder.SaveTo(FolderInfo.FullName, UserProfileData);
        }
    }*/
}