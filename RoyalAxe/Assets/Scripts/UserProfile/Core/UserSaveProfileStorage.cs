#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Core.UserProfile
{
    public interface IUserSaveProfileStorage
    {
        IUserProfileSave Current { get; }
        IReadOnlyCollection<IUserProfileSave> AllSaves { get; }
        IUserProfileSave CreateNew();
        IUserProfileSave GetSave(string folderName = null);
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage
    {
        private readonly UserProfileSave _current;
        private readonly IUserProfileSaveFactory _saveFactory;


        private readonly IUserSavePathSettings _savePathSettings;

        private readonly List<IUserProfileSave> _saves = new List<IUserProfileSave>();

        public UserSaveProfileStorage(IUserSavePathSettings savePathSettings, IUserProfileSaveFactory saveFactory)
        {
            _savePathSettings = savePathSettings;
            _saveFactory      = saveFactory;
            var root = LoadDirectory();
            _current = FactoryCreateSave("Current");
            foreach (var saveFolder in root.EnumerateDirectories()) GetSave(saveFolder.Name);
        }

        public IUserProfileSave Current => _current;
        public IReadOnlyCollection<IUserProfileSave> AllSaves => _saves;

        public IUserProfileSave CreateNew()
        {
            return GetSave($"Game_{_saves.Count}");
        }

        public IUserProfileSave GetSave(string saveFolderName)
        {
            if (string.IsNullOrEmpty(saveFolderName))
            {
                return null;
            }

            if (_current.Name == saveFolderName)
            {
                return _current;
            }

            var result = _saves.FirstOrDefault(o => o.Name == saveFolderName);
            if (result == null)
            {
                result = FactoryCreateSave(saveFolderName);
                _saves.Add(result);
            }

            return result;
        }

        private UserProfileSave FactoryCreateSave(string nameSave)
        {
            return _saveFactory.Create(new UserProfileData {FolderPath = new DirectoryInfo($"{_savePathSettings.RootPath}{nameSave}")});
        }

        private DirectoryInfo LoadDirectory()
        {
            return !Directory.Exists(_savePathSettings.RootPath) ? Directory.CreateDirectory(_savePathSettings.RootPath) : new DirectoryInfo(_savePathSettings.RootPath);
        }
    }
}