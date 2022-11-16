#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Core.UserProfile
{
    public interface IUserSaveProfileStorage
    {
        UserProfileData Current { get; }
        IReadOnlyCollection<UserProfileData> AllSaves { get; }
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage
    {
        public UserProfileData Current => _current;
        
        public IReadOnlyCollection<UserProfileData> AllSaves => _saves;
        private UserProfileData _current;
        private readonly IUserProfileBuilder<UserProfileData> _saveBuilder;
        private readonly IUserSavePathSettings _savePathSettings;

        private readonly List<UserProfileData> _saves = new List<UserProfileData>();

        public UserSaveProfileStorage(IUserSavePathSettings savePathSettings, IUserProfileBuilder<UserProfileData> saveFactory)
        {
            _savePathSettings = savePathSettings;
            _saveBuilder      = saveFactory;
            var root = LoadDirectory();
            foreach (var saveFolder in root.EnumerateDirectories()) GetSave(saveFolder.Name);
            _current = _saves.FirstOrDefault(o => o.IsLastPlayed) ?? FactoryCreateSave("TempCurrent");
        }

        UserProfileData GetSave(string saveFolderName)
        {
            if (string.IsNullOrEmpty(saveFolderName))
            {
                return null;
            }
            
            if (_current.FolderPath.Name == saveFolderName)
            {
                return _current;
            }

            var result = _saves.FirstOrDefault(o => o.FolderPath.Name == saveFolderName);
            if (result == null)
            {
                result = FactoryCreateSave(saveFolderName);
                _saves.Add(result);
            }
            return result;
        }

        private UserProfileData FactoryCreateSave(string nameSave)
        {
            var info = new DirectoryInfo($"{_savePathSettings.RootPath}{nameSave}");

            var result = new UserProfileData()
            {
                FolderPath = info
            };
            _saveBuilder.BuildFrom(result, info.FullName);
            return result;
        }

        private DirectoryInfo LoadDirectory()
        {
            return !Directory.Exists(_savePathSettings.RootPath) ? Directory.CreateDirectory(_savePathSettings.RootPath) : new DirectoryInfo(_savePathSettings.RootPath);
        }
    }
}