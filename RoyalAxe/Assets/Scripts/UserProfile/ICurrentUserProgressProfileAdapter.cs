namespace Core.UserProfile
{
    public interface ICurrentUserProgressProfileFacade
    {
        string ProfileName { get; }
        IUserLevelsProgress LevelProgressFacade { get; }
        IUserProfileHeroesProgress HeroesProgress { get; }
        IUserProfileWeaponsProgress WeaponProgress { get; }
    }

    public interface IUserProgressProfile
    {
        void Save();
    }
}
