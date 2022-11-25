using System.Collections.Generic;
using GameKit;

namespace Core.UserProfile
{
    public abstract class GeneralUserProgressProfileFacade : ICurrentUserProgressProfileFacade, IUserProgressProfile
    {
        public string ProfileName { get; }
        public IUserLevelsProgress LevelProgressFacade => _userLevelsProgress;
        public IUserProfileHeroesProgress HeroesProgress => _heroesProgress;
        public IUserProfileWeaponsProgress WeaponProgress => _weaponsProgress;
        public IGeneralProfileProgress GeneralProgress => _generalProgress;

        public IInventoryProgress InventoryProgress => _inventoryProgress;

        private IInventoryProgress _inventoryProgress;

        private IGeneralProfileProgress _generalProgress;
        private IUserLevelsProgress _userLevelsProgress;
        private IUserProfileHeroesProgress _heroesProgress;
        private IUserProfileWeaponsProgress _weaponsProgress;

        private readonly HashSet<IUserProgressProfile> _progressProfiles = new HashSet<IUserProgressProfile>();

        public GeneralUserProgressProfileFacade(string profileName, IReadOnlyCollection<IUserProgressPartFacade> progressPartFacade)
        {
            ProfileName = profileName;
            InitProgress(progressPartFacade);
        }

        private void InitProgress(IReadOnlyCollection<IUserProgressPartFacade> progressPartFacade)
        {
            progressPartFacade.ForEach(e =>
                                       {
                                           switch (e)
                                           {
                                               case IGeneralProfileProgress generalProfileProgress:
                                                   Set(ref _generalProgress, generalProfileProgress);
                                                   break;
                                               case IUserLevelsProgress levelsProgress:
                                                   Set(ref _userLevelsProgress, levelsProgress);
                                                   break;
                                               case IUserProfileHeroesProgress heroesProgress:
                                                   Set(ref _heroesProgress, heroesProgress);
                                                   break;
                                               case IUserProfileWeaponsProgress weaponsProgress:
                                                   Set(ref _weaponsProgress, weaponsProgress);
                                                   break;
                                               case IInventoryProgress inventoryProgress:
                                                   Set(ref _inventoryProgress, inventoryProgress);
                                                   break;
                                           }
                                       });
        }


        private void Set<T>(ref T my, T newValue) where T : class, IUserProgressProfile
        {
            if (_progressProfiles.Contains(newValue))
            {
                return;
            }

            if (newValue != null)
            {
                _progressProfiles.Add(newValue);
            }
            else
            {
                _progressProfiles.Remove(my);
            }

            my = newValue;
        }


        public void Save()
        {
            _progressProfiles.ForEach(e => e.Save());
        }
    }

    public class CurrentGeneralUserProgressProfileFacade : GeneralUserProgressProfileFacade
    {
        public CurrentGeneralUserProgressProfileFacade(string profileName, IReadOnlyCollection<IUserProgressPartFacade> progressPartFacade) : base(profileName, progressPartFacade) { }
    }
}