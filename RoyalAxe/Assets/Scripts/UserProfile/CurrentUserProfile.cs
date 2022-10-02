namespace Core.UserProfile 
{
    public class CurrentUserProfile : ICurrentUserProfile
    {
        public HeroProgressData CurrentHeroData { get; } = new HeroProgressData();
        public WeaponProgressData CurrentWeaponData { get; } = new WeaponProgressData();
    }
}