using System.Linq;

namespace Core.UserProfile
{
    public class WeaponProfileProgressFacade : UserProgressPartFacade<UserAllWeaponsProgress>, IUserProfileWeaponsProgress
    {
        protected override string Key => "Weapons";

        public WeaponProgressData GetWeaponProgress(string weaponID)
        {
            return Progress.WeaponProgressData.FirstOrDefault(o => o.Id == weaponID);
        }

        public WeaponProfileProgressFacade(IUserProgressPartSaveLoader progressAdapter) : base(progressAdapter) { }
    }
}