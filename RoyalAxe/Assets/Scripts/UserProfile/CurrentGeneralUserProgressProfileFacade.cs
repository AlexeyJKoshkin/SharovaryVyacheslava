using System.Collections.Generic;
using System.Linq;
using GameKit;

namespace Core.UserProfile {
    public class CurrentGeneralUserProgressProfileFacade : ICurrentUserProgressProfileFacade,IUserProgressProfile
    {
        public string ProfileName { get; }
        public IUserLevelsProgress LevelProgressFacade
        {
            get => _userLevelsProgress;
            set => Set(ref _userLevelsProgress, value);
        }
        public IUserProfileHeroesProgress HeroesProgress 
        {
            get => _heroesProgress;
            set => Set(ref _heroesProgress, value);
        }
        public IUserProfileWeaponsProgress WeaponProgress 
        {
            get => _weaponsProgress;
            set => Set(ref _weaponsProgress, value);
        }


        private IUserLevelsProgress _userLevelsProgress;
        private IUserProfileHeroesProgress _heroesProgress;
        private IUserProfileWeaponsProgress _weaponsProgress;

        private readonly HashSet<IUserProgressProfile> _progressProfiles = new HashSet<IUserProgressProfile>();

        public CurrentGeneralUserProgressProfileFacade(string profileName)
        {
            ProfileName = profileName;
        }

       
        private void Set<T>(ref T my, T newValue) where T : IUserProgressProfile
        {
            if(_progressProfiles.Contains(newValue)) return;
            if (newValue != null)
                _progressProfiles.Add(newValue);
            else _progressProfiles.Remove(my);
            my = newValue;
        }


        public void Save()
        {
            _progressProfiles.ForEach(e=> e.Save());
        }
    }
}