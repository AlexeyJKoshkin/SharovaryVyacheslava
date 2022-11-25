using System.Collections.Generic;

namespace Core.UserProfile
{
    class HeroProgressDefaultFactory :  IDefaultProgressFactory<UserAllHeroesProgress>
    {
        public UserAllHeroesProgress CreateDefault()
        {
            var current = new HeroProgressData()
            {
                Id = "Default_Hero", Level = 1
            };

            return new UserAllHeroesProgress()
            {
                SavedHeroes = new List<HeroProgressData>(){current},
                SelectedHeroId = current.Id
            };
        }
    }
}
