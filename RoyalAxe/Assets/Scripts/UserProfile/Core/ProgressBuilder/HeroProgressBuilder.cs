using System.Collections.Generic;
using Core.Configs;

namespace Core.UserProfile {
    public class HeroProgressBuilder : UserProfileInfrastructureHelper<UserAllHeroesProgress>
    {
        public override string FileName => "Heroes";

        public override UserAllHeroesProgress GetItemToSave(UserProfileData saveobject)
        {
            return saveobject.HeroProgress;
        }

        public override void SetItemToResult(UserProfileData result, UserAllHeroesProgress item)
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