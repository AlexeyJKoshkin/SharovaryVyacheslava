#region

using System.Collections.Generic;
using GameKit;
using VContainer.Unity;

#endregion

namespace Core.UserProfile
{
    public interface IUserSaveProfileStorage : IUserProgressProfile
    {
        ICurrentUserProgressProfileFacade Current { get; }
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage, IInitializable, ICurrentUserProgressProfileFacade
    {
        public string ProfileName => Current.ProfileName;
        public IGeneralProfileProgress GeneralProgress => Current.GeneralProgress;
        public IUserLevelsProgress LevelProgressFacade => Current.LevelProgressFacade;
        public IUserProfileHeroesProgress HeroesProgress => Current.HeroesProgress;
        public IUserProfileWeaponsProgress WeaponProgress => Current.WeaponProgress;
        public IInventoryProgress InventoryProgress => Current.InventoryProgress;
        public ICurrentUserProgressProfileFacade Current { get; private set; }

        private readonly IProfileProgressPartContextFactory _factory;
        private readonly IReadOnlyCollection<IUserProgressPartFacade> _progressPartFacade;

        private readonly HashSet<IUserProgressPartFacade> _saveData = new HashSet<IUserProgressPartFacade>();

        public UserSaveProfileStorage(IProfileProgressPartContextFactory factory,
                                      IReadOnlyList<IUserProgressPartFacade> partFacade)
        {
            _factory            = factory;
            _progressPartFacade = partFacade;
        }

        public void Initialize()
        {
            Current = Load("DefaultProfile");
            _progressPartFacade.ForEach(e => e.OnSaveProgress += SaveProgressHandler);
        }

        public void Save()
        {
            if (_saveData.Count > 0)
            {
                var context = _factory.CreateLocale(Current.ProfileName); //создаем контекст текущего профайла
                _saveData.ForEach(e => e.SaveProgress(context));
                _saveData.Clear();
            }
        }

        private void SaveProgressHandler(IUserProgressPartFacade progressSaveLoader)
        {
            _saveData.Add(progressSaveLoader);
        }


        private ICurrentUserProgressProfileFacade Load(string profileName)
        {
            var context = _factory.CreateLocale(profileName); // Создаем текущий контекст для загрузки профиля
            _progressPartFacade.ForEach(e => e.LoadProgress(context));// грузим все из контекста
            var result = new CurrentGeneralUserProgressProfileFacade(profileName, _progressPartFacade); // создаем фасад сохранения
            return result;
        }
    }
}