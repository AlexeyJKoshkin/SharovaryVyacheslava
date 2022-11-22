using System.Linq;

namespace Core.UserProfile {
    public class HeroProfileProgressFacade : UserProfileProgressFacade<UserAllHeroesProgress>,IUserProfileHeroesProgress
    {
        public HeroProfileProgressFacade(IUserProgressPartFactory<UserAllHeroesProgress> loader) : base(loader) { }
        
        protected override string Key => "Heroes";
        public HeroProgressData CurrentHero { get; private set; }
        protected override void UpdateProgressData()
        {
            CurrentHero = GetCurrentHero();
        }

        protected override void SetToMainFacade(CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            currentGeneralUserProgressProfileFacade.HeroesProgress = this;
        }

        private HeroProgressData GetCurrentHero()
        {
            if (string.IsNullOrEmpty(Progress.SelectedHeroId))
            {
                return Progress.SavedHeroes.FirstOrDefault();
            }
            return
                Progress.SavedHeroes.FirstOrDefault(data => data.CharacterRecord.Id == Progress.SelectedHeroId);
        }
        
        public HeroProgressData GetHero(string heroId)
        {
            return Progress.SavedHeroes.FirstOrDefault(o => o.CharacterRecord.Id == heroId);
        }
        
        public HeroProgressData SelectHero(string heroId)
        {
            if (string.IsNullOrEmpty(heroId)) return null;
            var result = GetHero(heroId);
            if (result == null) return null;
            CurrentHero                  = result;
            Progress.SelectedHeroId = heroId;
            SetDirty();
            return CurrentHero;
        }
        
    }
}