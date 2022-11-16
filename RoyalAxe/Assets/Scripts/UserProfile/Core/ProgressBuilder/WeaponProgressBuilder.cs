using System.Collections.Generic;
using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProgressBuilder : UserProfilePartBuilder<UserAllWeaponsProgress>
    {

        protected override string FileName => "Weapons";

        protected override UserAllWeaponsProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.WeaponProgress;
        }

        protected override void SetItemToResult(UserProfileData result, UserAllWeaponsProgress item)
        {
            result.WeaponProgress = item;
        }

        public WeaponProgressBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter, IDefaultProgressFactory<UserAllWeaponsProgress> defaultProgressFactory) : base(jsonFileOperation, jsonConverter, defaultProgressFactory) { }
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