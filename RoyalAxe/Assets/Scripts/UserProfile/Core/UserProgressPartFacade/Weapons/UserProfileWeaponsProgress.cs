using System.Linq;

namespace Core.UserProfile
{
    public interface IUserProfileWeaponsProgress : IUserProgressProfile
    {
        WeaponProgressData GetWeaponProgress(string weaponID);
    }
    
    public class UserProfileWeaponsProgress : IUserProfileWeaponsProgress
    {
        private readonly UserAllWeaponsProgress _progressData;

        public UserProfileWeaponsProgress(UserAllWeaponsProgress progressData)
        {
            _progressData = progressData;

        }

        public WeaponProgressData GetWeaponProgress(string heroId)
        {
            return _progressData.ProgressData.FirstOrDefault(o => o.WeaponID == heroId);
        }
    }
}
