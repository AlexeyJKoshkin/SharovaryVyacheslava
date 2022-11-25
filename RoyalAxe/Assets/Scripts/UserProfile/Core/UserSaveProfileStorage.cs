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

    public class UserSaveProfileStorage : IUserSaveProfileStorage,  IInitializable,ICurrentUserProgressProfileFacade
    {
        public string ProfileName => _current.ProfileName;
        public IGeneralProfileProgress GeneralProgress => _current.GeneralProgress;
        public IUserLevelsProgress LevelProgressFacade => _current.LevelProgressFacade;
        public IUserProfileHeroesProgress HeroesProgress => _current.HeroesProgress;
        public IUserProfileWeaponsProgress WeaponProgress => _current.WeaponProgress;
        public IInventoryProgress InventoryProgress => _current.InventoryProgress;
        public ICurrentUserProgressProfileFacade Current => _current;

        private ICurrentUserProgressProfileFacade _current;

        private readonly IProfileProgressPartContextFactory _factory;
        private readonly IReadOnlyList<IUserProgressSaveLoader> _progressSaveLoaderAdapter;
        private readonly IReadOnlyCollection<IUserProgressPartFacade> _progressPartFacade;

        private readonly HashSet<IUserProgressSaveLoader> _saveData = new HashSet<IUserProgressSaveLoader>();

        public UserSaveProfileStorage(IProfileProgressPartContextFactory factory,
                                      IReadOnlyCollection<IUserProgressPartFacade> partFacade,
                                      IReadOnlyList<IUserProgressSaveLoader> progressSaveLoaderAdapter)
        {
            _factory = factory;
            _progressPartFacade = partFacade;
            _progressSaveLoaderAdapter = progressSaveLoaderAdapter;
       }
        public void Initialize()
        {
            _current = Load("DefaultProfile");
            _progressSaveLoaderAdapter.ForEach(e=> e.OnSaveProgress += SaveProgressHandler);
        }

        void SaveProgressHandler(IUserProgressSaveLoader progressSaveLoader)
        {
            _saveData.Add(progressSaveLoader);
        }


        ICurrentUserProgressProfileFacade Load(string profileName)
        {
            var context = _factory.CreateLocale(profileName); // Создаем текущий контекст для загрузки профиля
            var result = new CurrentGeneralUserProgressProfileFacade(profileName);
            _progressSaveLoaderAdapter.ForEach((e=> e.LoadProgress(context, result)));
            _progressPartFacade.ForEach(e=> e.InitTo(result));
            return result;
        }


        public void Save()
        {
            if (_saveData.Count > 0)
            {
                var context = _factory.CreateLocale(_current.ProfileName); //создаем контекст текущего профайла
                _saveData.ForEach(e=> e.SaveProgress(context));
                _saveData.Clear();
            }
        }

   
        
    }
}
