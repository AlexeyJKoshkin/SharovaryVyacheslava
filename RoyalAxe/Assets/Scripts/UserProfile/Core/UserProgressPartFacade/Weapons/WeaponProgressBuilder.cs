using System.Linq;
using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProfileProgressFacade : UserProfileProgressFacade<UserAllWeaponsProgress>,IUserProfileWeaponsProgress
    {
        protected override string Key => "Weapons";

        public WeaponProfileProgressFacade(IUserProgressPartFactory<UserAllWeaponsProgress> loader) : base(loader) { }
        public WeaponProgressData GetWeaponProgress(string weaponID)
        {
            return Progress.WeaponProgressData.FirstOrDefault(o => o.Weapon.Id == weaponID);
        }
        protected override void UpdateProgressData()
        {
        }

        protected override void SetToMainFacade(CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.WeaponProgress = this;
        }
    }
}