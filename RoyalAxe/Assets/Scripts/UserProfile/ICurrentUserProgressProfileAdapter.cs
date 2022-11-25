namespace Core.UserProfile
{
    public interface ICurrentUserProgressProfileFacade
    {
        string ProfileName { get; }
        IGeneralProfileProgress GeneralProgress { get; }
        IUserLevelsProgress LevelProgressFacade { get; }
        IUserProfileHeroesProgress HeroesProgress { get; }
        IUserProfileWeaponsProgress WeaponProgress { get; }
        IInventoryProgress InventoryProgress { get; }
    }

    public interface IUserProgressProfile
    {
        void Save();
    }
}