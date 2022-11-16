using System.Collections.Generic;
using Core.Configs;

namespace Core.UserProfile {
    public class HeroProgressBuilder : UserProfilePartBuilder<UserAllHeroesProgress>
    {
        public HeroProgressBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter, IDefaultProgressFactory<UserAllHeroesProgress> defaultProgressFactory) : base(jsonFileOperation, jsonConverter, defaultProgressFactory) { }
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

    class HeroProgressBuilderDefaultBuilder :  IDefaultProgressFactory<UserAllHeroesProgress>
    {
        public UserAllHeroesProgress CreateDefault()
        {
            var current = new HeroProgressData();

            return new UserAllHeroesProgress()
            {
                ProgressData = new List<HeroProgressData>(){current},
                SelectedHeroId = current.CharacterId
            };
        }
    }
}