using System.Collections.Generic;
using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProgressBuilder : UserProfileInfrastructureHelper<UserAllWeaponsProgress>
    {

        public override string FileName => "Weapons";

        public override UserAllWeaponsProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.WeaponProgress;
        }

        public override void SetItemToResult(UserProfileData result, UserAllWeaponsProgress item)
        {
            result.WeaponProgress = item;
        }
    }

    public class WeaponProgressBuilderDefault : IDefaultProgressFactory<UserAllWeaponsProgress>
    {
        public UserAllWeaponsProgress CreateDefault()
        {
            var current = new WeaponProgressData();

            return new UserAllWeaponsProgress()
            {
                ProgressData = new List<WeaponProgressData>(){current},
                SelectedWeaponId = current.WeaponID
            };
        }
    }
}