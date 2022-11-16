namespace Core.UserProfile
{
    public interface ICurrentUserProgressProfileAdapter
    {
        string ProfileName { get; }
        bool IsLastPlayed { get; }
    }

    public class CurrentUserProgressProfileAdapter : ICurrentUserProgressProfileAdapter
    {
        public string ProfileName => _userProfileData.FolderPath.Name;
        public bool IsLastPlayed => _userProfileData.IsLastPlayed;
        private readonly UserProfileData _userProfileData;

        public CurrentUserProgressProfileAdapter(UserProfileData userProfileData)
        {
            _userProfileData = userProfileData;
        }

        
    }
}
