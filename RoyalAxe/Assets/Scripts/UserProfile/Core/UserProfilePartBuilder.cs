#region

using System;
using System.Threading.Tasks;
using Core.Configs;

#endregion

namespace Core.UserProfile
{
    //Контекстное хранилище текущего профиля
    public interface IProfileProgressStorageContext
    {
        void Save(string json, string key);
        string Load(string key);
    }
    
    public interface IProfileProgressPartContextFactory
    {
        LocalUserProfileContext CreateLocale(UserProfileData profile);
    }

    public class ProfileProgressPartContextFactory : IProfileProgressPartContextFactory
    {
        private readonly IUserSavePathSettings _settings;
        private readonly ITextFileOperation _textFileOperation;
        public ProfileProgressPartContextFactory(ITextFileOperation textFileOperation, IUserSavePathSettings settings)
        {
            _textFileOperation = textFileOperation;
            _settings = settings;
        }

        public LocalUserProfileContext CreateLocale(UserProfileData profile)
        {
            return new LocalUserProfileContext(_textFileOperation, _settings, profile);
        }
    }



    public class LocalUserProfileContext : IProfileProgressStorageContext
    {
        private readonly UserProfileData _userProfile;
        private readonly IUserSavePathSettings _settings;
        private readonly ITextFileOperation _textFileOperation;
        public LocalUserProfileContext(ITextFileOperation textFileOperation, IUserSavePathSettings settings, UserProfileData userProfile)
        {
            _textFileOperation = textFileOperation;
            _settings = settings;
            _userProfile = userProfile;
        }

        void IProfileProgressStorageContext.Save(string json, string key)
        {
            string pathToFile = $"{_settings.RootPath}{_userProfile.ProfileName}/{key}.json";
            _textFileOperation.Save(pathToFile, json);
        }

        string IProfileProgressStorageContext.Load(string key)
        {
            string pathToFile = $"{_settings.RootPath}{_userProfile.ProfileName}/{key}.json";
            return _textFileOperation.LoadText(pathToFile).Result;
        }
    }


    public abstract class UserProfileInfrastructureHelper<TData> : IUserProfileInfrastructure<TData> where TData :BaseUserProgressData
    {

        public abstract string FileName { get; }

        public abstract TData GetItemToSave(UserProfileData saveobject);
        

        public abstract void SetItemToResult(UserProfileData result, TData item);
    }

  
}