using System.Linq;
using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProfileProgressSaveLoaderAdapter : UserProfileProgressSaveLoaderAdapter<UserAllWeaponsProgress>,IUserProfileWeaponsProgress
    {
        protected override string Key => "Weapons";

        public WeaponProfileProgressSaveLoaderAdapter(IUserProgressPartFactory<UserAllWeaponsProgress> loader) : base(loader) { }
        public WeaponProgressData GetWeaponProgress(string weaponID)
        {
            return Progress.WeaponProgressData.FirstOrDefault(o => o.Id == weaponID);
        }

        protected override void SetToMainFacade(GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.WeaponProgress = this;
        }
    }
}