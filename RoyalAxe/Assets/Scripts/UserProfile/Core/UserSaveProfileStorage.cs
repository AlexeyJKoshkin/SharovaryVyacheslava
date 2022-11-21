#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Configs;
using TinyJSON;
using UnityEngine;
using VContainer.Unity;
using static UnityEngine.PlayerPrefs;

#endregion

namespace Core.UserProfile
{
    public interface IUserSaveProfileStorage
    {
        ICurrentUserProgressProfileFacade Current { get; }
        void ReloadSaves();
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage, ICurrentUserProgressProfileFacade
    {
        public ICurrentUserProgressProfileFacade Current => _current;

        private ICurrentUserProgressProfileFacade _current;

        private readonly IProfileProgressPartContextFactory _factory;
        private readonly IReadOnlyList<IUserProgressPartFacade> _builders;

        public UserSaveProfileStorage(IProfileProgressPartContextFactory factory, IReadOnlyList<IUserProgressPartFacade> builders)
        {
            _factory = factory;
            _builders = builders;
        }

        public void ReloadSaves()
        {
            _current = Load("DefaultProfile");
        }

        ICurrentUserProgressProfileFacade Load(string profileName)
        {
            var context = _factory.CreateLocale(profileName); // Создаем текущий контекст для загрузки профиля
            var result = new CurrentUserProgressProfileFacade(profileName);

            foreach (var builder in _builders)
            {
                builder.LoadProgress(context, result);
            }
            return result;
        }

        public string ProfileName => _current.ProfileName;
        public T Get<T>()
        {
            return _current.Get<T>();
        }
    }
}
