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
       // IReadOnlyCollection<UserProfileData> AllSaves { get; }

       void Load(string name);
       void SaveCurrent();

       /*UserProfileData CreateNew();
       UserProfileData GetSave(string folderName = null);*/
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage
    {
        private readonly UserProfileData _current;
        private readonly IUserProfileSaveFactory _saveFactory;


        private readonly IUserSavePathSettings _savePathSettings;

        private readonly List<UserProfileData> _saves = new List<UserProfileData>();

        public UserSaveProfileStorage(IUserSavePathSettings savePathSettings, IUserProfileSaveFactory saveFactory)
        {
            _savePathSettings = savePathSettings;
            _saveFactory      = saveFactory;
            var root = LoadDirectory();
            foreach (var saveFolder in root.EnumerateDirectories()) GetSave(saveFolder.Name);
            _current = GetSave("Current");
          
        }


        public UserProfileData Current => _current;
        public void SaveCurrent()
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<UserProfileData> AllSaves => _saves;

        public UserProfileData CreateNew()
        {
            return GetSave($"Game_{_saves.Count}");
        }

        public UserProfileData GetSave(string saveFolderName)
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
            return _saveFactory.Create(new DirectoryInfo($"{_savePathSettings.RootPath}{nameSave}"));
        }

        private DirectoryInfo LoadDirectory()
        {
            return !Directory.Exists(_savePathSettings.RootPath) ? Directory.CreateDirectory(_savePathSettings.RootPath) : new DirectoryInfo(_savePathSettings.RootPath);
        }
    }
}