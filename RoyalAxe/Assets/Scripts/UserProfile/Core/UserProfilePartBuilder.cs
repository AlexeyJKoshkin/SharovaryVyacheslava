#region

using System;
using System.Threading.Tasks;
using Core.Configs;

#endregion

namespace Core.UserProfile
{
    public abstract class UserProfilePartBuilder<TData> : IUserProfilePartBuilder<UserProfileData> where TData :BaseUserProgressData
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly ITextFileOperation _jsonFileOperation;

        private readonly IDefaultProgressFactory<TData> _defaultProgressFactory;

        public UserProfilePartBuilder(ITextFileOperation jsonFileOperation,
                                      IJsonConverter jsonConverter,
                                      IDefaultProgressFactory<TData> defaultProgressFactory)
        {
            _jsonFileOperation = jsonFileOperation;
            _jsonConverter     = jsonConverter;
            _defaultProgressFactory = defaultProgressFactory;
        }

        protected abstract string FileName { get; }


        public void SaveTo(string folderInfoFullName, UserProfileData saveobject)
        {
            var item = GetItemToSave(saveobject);
            var json = _jsonConverter.SerializeObject(item);
            var path = $"{folderInfoFullName}/{FileName}.json";
            _jsonFileOperation.Save(path, json);
        }

        public async Task LoadFrom(string folderInfoFullName, UserProfileData userProfile)
        {
            var path = $"{folderInfoFullName}/{FileName}.json";
            var json = await _jsonFileOperation.LoadText(path);
            var item =  Parse(json);
            SetItemToResult(userProfile, item);
        }

        private TData Parse(string json)
        {
            if (string.IsNullOrEmpty(json)) return _defaultProgressFactory.CreateDefault();

            try
            {
                return _jsonConverter.Deserialize<TData>(json);
            }
            catch (Exception e)
            {
               HLogger.LogException(e);
               return _defaultProgressFactory.CreateDefault();
            }
        }

        protected abstract TData GetItemToSave(UserProfileData saveobject);
        

        protected abstract void SetItemToResult(UserProfileData result, TData item);
    }
}