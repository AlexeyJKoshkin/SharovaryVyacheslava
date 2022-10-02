using Core.Configs;

namespace Core.UserProfile {
    public class HeroProgressBuilder : UserProfilePartBuilder<UserAllHeroesProgress>
    {
        public HeroProgressBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter) : base(jsonFileOperation, jsonConverter) { }

        protected override string FileName => "Heroes";

        protected override UserAllHeroesProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.HeroProgress;
        }

        protected override void SetItemToResult(UserProfileData result, UserAllHeroesProgress item)
        {
            result.HeroProgress = item;
        }
    }
}