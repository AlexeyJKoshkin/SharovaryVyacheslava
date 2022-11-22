
namespace Core.UserProfile
{
    public interface IUserProfileWeaponsProgress : IUserProgressProfile
    {
        WeaponProgressData GetWeaponProgress(string weaponID);
    }
}
