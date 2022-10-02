#region

using System.IO;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfileSave
    {
        string Name { get; }
        UserProfileData UserProfileData { get; }
        void Save();
    }

    public class UserProfileSave : IUserProfileSave
    {
        private readonly IDataFromJsonBuilder<UserProfileData> _builder;

        public UserProfileSave(UserProfileData userProfileData, IDataFromJsonBuilder<UserProfileData> builder)
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

        private DirectoryInfo FolderInfo => UserProfileData.FolderPath;

        public string Name => FolderInfo.Name;
        public UserProfileData UserProfileData { get; }

        public void Save()
        {
            _builder.SaveTo(FolderInfo.FullName, UserProfileData);
        }
    }
}