using System.Collections.Generic;
using System.Linq;
using GameKit;

namespace Core.UserProfile 
{
    public abstract class GeneralUserProgressProfileFacade : ICurrentUserProgressProfileFacade, IUserProgressProfile
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
        public IGeneralProfileProgress GeneralProgress  
        {
            get => _generalProgress;
            set => Set(ref _generalProgress, value);
        }
       
        public IInventoryProgress InventoryProgress  
        {
            get => _inventoryProgress;
            set => Set(ref _inventoryProgress, value);
        }
       
        private IInventoryProgress _inventoryProgress;

        private IGeneralProfileProgress _generalProgress;
        private IUserLevelsProgress _userLevelsProgress;
        private IUserProfileHeroesProgress _heroesProgress;
        private IUserProfileWeaponsProgress _weaponsProgress;

        private readonly HashSet<IUserProgressProfile> _progressProfiles = new HashSet<IUserProgressProfile>();

        public GeneralUserProgressProfileFacade(string profileName)
        {
            ProfileName = profileName;
        }

       
        private void Set<T>(ref T my, T newValue) where T : class,IUserProgressProfile 
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

    public class CurrentGeneralUserProgressProfileFacade : GeneralUserProgressProfileFacade
    {
        public CurrentGeneralUserProgressProfileFacade(string profileName) : base(profileName) { }
    }
}