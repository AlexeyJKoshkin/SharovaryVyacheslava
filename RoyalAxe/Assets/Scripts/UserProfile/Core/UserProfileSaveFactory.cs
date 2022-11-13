using System.IO;

namespace Core.UserProfile
{
    public interface IUserProfileSaveFactory
    {
        UserProfileData Create(DirectoryInfo userProfileData);
    }

    public class UserProfileSaveFactory : IUserProfileSaveFactory
    {
        private readonly IUserProfileBuilder<UserProfileData> _builder;

        private UserProfileSaveFactory(IUserProfileBuilder<UserProfileData> builder)
        {
            _builder = builder;
        }

        public UserProfileData Create(UserProfileData userProfileData)
        {
            var result = new UserProfileSave(userProfileData, _builder);
            if(result.UserProfileData.)
        }

        public UserProfileData Create(DirectoryInfo userProfileData)
        {
            var result = new UserProfileData() {FolderPath = userProfileData};
            _builder.BuildFrom(result, userProfileData.FullName);
        }
    }
}