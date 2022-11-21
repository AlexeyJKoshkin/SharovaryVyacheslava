namespace Core.UserProfile
{
    public interface ICurrentUserProgressProfileAdapter
    {
        string ProfileName { get; }
        bool IsLastPlayed { get; }
        HeroProgressData CurrentHero { get; }
    }
    
    

    public class CurrentUserProgressProfileAdapter : ICurrentUserProgressProfileAdapter
    {
        public string ProfileName => _userProfileData.ProfileName;
        public bool IsLastPlayed => _userProfileData.IsLastPlayed;
        public HeroProgressData CurrentHero { get; private set; }
        private readonly UserProfileData _userProfileData;

        public CurrentUserProgressProfileAdapter(UserProfileData userProfileData)
        {
            _userProfileData = userProfileData;
            CurrentHero = userProfileData.LoadCurrentHero();
        }

        
    }
}
