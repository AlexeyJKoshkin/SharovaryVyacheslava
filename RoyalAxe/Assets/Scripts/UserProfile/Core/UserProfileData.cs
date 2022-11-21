#region

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfileData
    {
        
    }

    public class UserProfileData : IUserProfileData
    {
        public bool IsLastPlayed;
        public string ProfileName;

        public UserAllHeroesProgress HeroProgress;
        public UserAllWeaponsProgress WeaponProgress;
        public UserLevelProgress LevelProgress;
    }




    [Serializable]
    public abstract class BaseUserProgressData
    {
        //версия структуры
        public int FormatVersion { get; set; }
        
        //имя файла в котором хранится прогресс
        //public abstract string FileName { get; }
    }
}