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
        public IUserLevelsProgress LevelProgressFacade => _current.LevelProgressFacade;
        public IUserProfileHeroesProgress HeroesProgress => _current.HeroesProgress;
        public IUserProfileWeaponsProgress WeaponProgress => _current.WeaponProgress;
        public ICurrentUserProgressProfileFacade Current => _current;

        private ICurrentUserProgressProfileFacade _current;

        private readonly IProfileProgressPartContextFactory _factory;
        private readonly IReadOnlyList<IUserProgressPartFacade> _progressFacade;

        private readonly HashSet<IUserProgressPartFacade> _saveData = new HashSet<IUserProgressPartFacade>();

        public UserSaveProfileStorage(IProfileProgressPartContextFactory factory, IReadOnlyList<IUserProgressPartFacade> progressFacade)
        {
            _factory = factory;
            _progressFacade = progressFacade;
       }
        public void Initialize()
        {
            _current = Load("DefaultProfile");
            _progressFacade.ForEach(e=> e.OnSaveProgress += SaveProgressHandler);
        }

        void SaveProgressHandler(IUserProgressPartFacade progressPartFacade)
        {
            _saveData.Add(progressPartFacade);
        }


        ICurrentUserProgressProfileFacade Load(string profileName)
        {
            var context = _factory.CreateLocale(profileName); // Создаем текущий контекст для загрузки профиля
            var result = new CurrentGeneralUserProgressProfileFacade(profileName);
            _progressFacade.ForEach((e=> e.LoadProgress(context, result)));
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
