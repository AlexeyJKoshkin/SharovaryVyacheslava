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
        ICurrentUserProgressProfileAdapter Current { get; }
        void ReloadSaves();
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage
    {
        public ICurrentUserProgressProfileAdapter Current => _current;

        private ICurrentUserProgressProfileAdapter _current;

        private readonly IUserSaveProfileCRUDCommand<UserProfileData> _crudCommand;


        public UserSaveProfileStorage(IUserSaveProfileCRUDCommand<UserProfileData> crudCommand)
        {
            _crudCommand = crudCommand;
        }

        public void ReloadSaves()
        {
            var userProfileData = _crudCommand.Read("DefaultProfile") ?? _crudCommand.Create("DefaultProfile");
            _current = new CurrentUserProgressProfileAdapter(userProfileData);
       
        }

    }
}