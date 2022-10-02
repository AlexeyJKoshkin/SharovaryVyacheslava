using Core.Configs;

namespace Core.UserProfile 
{
    public class WeaponProgressBuilder : UserProfilePartBuilder<UserAllWeaponsProgress>
    {
        public WeaponProgressBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter) : base(jsonFileOperation, jsonConverter) { }

        protected override string FileName => "Weapons";

        protected override UserAllWeaponsProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.WeaponProgress;
        }

        protected override void SetItemToResult(UserProfileData result, UserAllWeaponsProgress item)
        {
            result.WeaponProgress = item;
        }
    }
}