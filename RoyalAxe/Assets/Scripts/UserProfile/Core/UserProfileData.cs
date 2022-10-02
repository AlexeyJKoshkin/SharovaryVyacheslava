#region

using System.IO;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfileData
    {
        
    }

    public class UserProfileData : IUserProfileData
    {
        public DirectoryInfo FolderPath;

        public UserAllHeroesProgress HeroProgress;

        public UserAllWeaponsProgress WeaponProgress;

    }
}