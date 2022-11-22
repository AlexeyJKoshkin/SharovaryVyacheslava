namespace Core.UserProfile {
    public interface IUserProfileHeroesProgress : IUserProgressProfile
    {
        HeroProgressData CurrentHero { get; }
    }
}