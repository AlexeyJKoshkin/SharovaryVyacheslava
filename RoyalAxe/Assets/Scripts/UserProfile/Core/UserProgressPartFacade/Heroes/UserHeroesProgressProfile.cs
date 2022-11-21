using System.Linq;

namespace Core.UserProfile
{
    public interface IUserProfileHeroesProgress : IUserProgressProfile
    {
        HeroProgressData CurrentHero { get; }
    }
    
    public class UserHeroesProgressProfile : IUserProgressProfile
    {
        private readonly UserAllHeroesProgress _progressData;
        public HeroProgressData CurrentHero { get; private set; }
        

        public UserHeroesProgressProfile(UserAllHeroesProgress progressData)
        {
            _progressData = progressData;

            CurrentHero = progressData.ProgressData.DefaultFind(_progressData.SelectedHeroId,
                                                                data => data.CharacterId == _progressData.SelectedHeroId);

        }

        public HeroProgressData GetHero(string heroId)
        {
            return _progressData.ProgressData.FirstOrDefault(o => o.CharacterId == heroId);
        }
        
        public HeroProgressData SelectHero(string heroId)
        {
            if (string.IsNullOrEmpty(heroId)) return null;
            var result = GetHero(heroId);
            if (result == null) return null;
            CurrentHero = result;
            _progressData.SelectedHeroId = heroId;
            return CurrentHero;
        }
    }
}
