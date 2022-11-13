using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.UserProfile
{
    public class UserProfileDataCompositeBuilder : IUserProfileBuilder<UserProfileData>
    {
        private readonly IReadOnlyList<IUserProfilePartBuilder<UserProfileData>> _builders;

        public UserProfileDataCompositeBuilder(IReadOnlyList<IUserProfilePartBuilder<UserProfileData>> builders)
        {
            _builders = builders;
        }


        public void SaveTo(string folderInfoFullName, UserProfileData saveobject)
        {
            foreach (var builder in _builders) builder.SaveTo(folderInfoFullName, saveobject);
        }

        public async Task BuildFrom(UserProfileData result, string folderInfoFullName)
        {
            foreach (var builder in _builders) await builder.LoadFrom(folderInfoFullName, result);
        }
    }
}