#region

using System;
using System.IO;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfileData
    {
        
    }

    public class UserProfileData : IUserProfileData
    {
        public bool IsLastPlayed;
        public DirectoryInfo FolderPath;
        public UserAllHeroesProgress HeroProgress;
        public UserAllWeaponsProgress WeaponProgress;
        public UserLevelProgress LevelProgress;
    }



    [Serializable]
    public class BaseUserProgressData
    {
        public int Version { get; set; }
    }
}