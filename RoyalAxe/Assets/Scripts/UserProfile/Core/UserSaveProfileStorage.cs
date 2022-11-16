#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Core.UserProfile
{
    public interface IUserSaveProfileStorage
    {
        ICurrentUserProgressProfileAdapter Current { get; }
        IReadOnlyCollection<ICurrentUserProgressProfileAdapter> AllSaves { get; }
    }

    public class UserSaveProfileStorage : IUserSaveProfileStorage
    {
        public ICurrentUserProgressProfileAdapter Current => _current;
        
        public IReadOnlyCollection<ICurrentUserProgressProfileAdapter> AllSaves => _saves;
        private ICurrentUserProgressProfileAdapter _current;
        private readonly IUserProfileBuilder<UserProfileData> _saveBuilder;
        private readonly IUserSavePathSettings _savePathSettings;

        private readonly List<ICurrentUserProgressProfileAdapter> _saves = new List<ICurrentUserProgressProfileAdapter>();

        public UserSaveProfileStorage(IUserSavePathSettings savePathSettings, IUserProfileBuilder<UserProfileData> saveFactory)
        {
            _savePathSettings = savePathSettings;
            _saveBuilder      = saveFactory;
            var root = LoadDirectory();
            foreach (var saveFolder in root.EnumerateDirectories()) GetSave(saveFolder.Name);
            _current = _saves.FirstOrDefault(o => o.IsLastPlayed) ?? FactoryCreateSave("TempCurrent");
        }

        ICurrentUserProgressProfileAdapter GetSave(string saveFolderName)
        {
            if (string.IsNullOrEmpty(saveFolderName))
            {
                return null;
            }
            
            if (_current.ProfileName == saveFolderName)
            {
                return _current;
            }

            var result = _saves.FirstOrDefault(o => o.ProfileName == saveFolderName);
            if (result == null)
            {
                result = FactoryCreateSave(saveFolderName);
                _saves.Add(result);
            }
            return result;
        }

        private ICurrentUserProgressProfileAdapter FactoryCreateSave(string nameSave)
        {
            var info = new DirectoryInfo($"{_savePathSettings.RootPath}{nameSave}");

            var result = new UserProfileData()
            {
                FolderPath = info
            };
            _saveBuilder.BuildFrom(result, info.FullName);
            return new CurrentUserProgressProfileAdapter(result);
        }

        private DirectoryInfo LoadDirectory()
        {
            return !Directory.Exists(_savePathSettings.RootPath) ? Directory.CreateDirectory(_savePathSettings.RootPath) : new DirectoryInfo(_savePathSettings.RootPath);
        }
    }
}