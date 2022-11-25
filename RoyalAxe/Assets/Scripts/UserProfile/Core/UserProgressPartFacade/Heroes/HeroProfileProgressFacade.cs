using System.Linq;

namespace Core.UserProfile
{
    public class HeroProfileProgressFacade : UserProgressPartFacade<UserAllHeroesProgress>, IUserProfileHeroesProgress
    {
        protected override string Key => "Heroes";
        public HeroProgressData CurrentHero => GetCurrentHero();

        private HeroProgressData GetCurrentHero()
        {
            if (string.IsNullOrEmpty(Progress.SelectedHeroId)) return Progress.SavedHeroes.FirstOrDefault();

            return Progress.SavedHeroes.FirstOrDefault(data => data.Id == Progress.SelectedHeroId);
        }

        public HeroProgressData GetHero(string heroId)
        {
            return Progress.SavedHeroes.FirstOrDefault(o => o.Id == heroId);
        }

        public HeroProgressData SelectHero(string heroId)
        {
            if (string.IsNullOrEmpty(heroId)) return null;

            var result = GetHero(heroId);
            if (result == null) return null;
            Progress.SelectedHeroId = heroId;
            SetDirty();
            return CurrentHero;
        }

        public HeroProfileProgressFacade(IUserProgressPartSaveLoader progressAdapter) : base(progressAdapter) { }
    }
}