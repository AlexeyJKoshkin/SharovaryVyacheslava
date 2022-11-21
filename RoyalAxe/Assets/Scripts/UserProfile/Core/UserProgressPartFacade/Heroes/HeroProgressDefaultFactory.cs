using System.Collections.Generic;

namespace Core.UserProfile
{
    class HeroProgressDefaultFactory :  IDefaultProgressFactory<UserAllHeroesProgress>
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
