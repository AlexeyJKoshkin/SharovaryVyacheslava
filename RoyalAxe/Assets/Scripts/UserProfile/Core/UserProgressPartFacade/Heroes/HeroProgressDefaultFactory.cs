using System.Collections.Generic;

namespace Core.UserProfile
{
    internal class HeroProgressDefaultFactory : BaseDefaultProgressFactory<UserAllHeroesProgress>
    {
        public override UserAllHeroesProgress CreateDefault()
        {
            var current = new HeroProgressData
            {
                Id = "Default_Hero", Level = 1
            };

            return new UserAllHeroesProgress
            {
                SavedHeroes    = new List<HeroProgressData> {current},
                SelectedHeroId = current.Id
            };
        }
    }
}