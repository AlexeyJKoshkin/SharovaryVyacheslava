#region

using System.Threading.Tasks;
using Core.Configs;

#endregion

namespace Core.UserProfile
{
    public abstract class UserProfilePartBuilder<TData> : IUserProfilePartBuilder<UserProfileData> where TData : new()
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly ITextFileOperation _jsonFileOperation;

        public UserProfilePartBuilder(ITextFileOperation jsonFileOperation, IJsonConverter jsonConverter)
        {
            _jsonFileOperation = jsonFileOperation;
            _jsonConverter     = jsonConverter;
        }

        protected abstract string FileName { get; }


        public void SaveTo(string folderInfoFullName, UserProfileData saveobject)
        {
            var item = GetItemToSave(saveobject);
            var json = _jsonConverter.SerializeObject(item);
            var path = $"{folderInfoFullName}/{FileName}.json";
            _jsonFileOperation.Save(path, json);
        }


        public async Task LoadFrom(string folderInfoFullName, UserProfileData result)
        {
            var path = $"{folderInfoFullName}/{FileName}.json";
            var json = await _jsonFileOperation.LoadText(path);
            var item = string.IsNullOrEmpty(json) ? new TData() : _jsonConverter.Deserialize<TData>(json);
            SetItemToResult(result, item);
        }

        protected abstract TData GetItemToSave(UserProfileData saveobject);

        protected abstract void SetItemToResult(UserProfileData result, TData item);
    }
}