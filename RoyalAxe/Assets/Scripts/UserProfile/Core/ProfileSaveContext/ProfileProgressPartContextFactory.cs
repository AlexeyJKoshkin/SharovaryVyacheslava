using System.IO;
using Core.Configs;
using VContainer.Unity;

namespace Core.UserProfile
{
    public interface IProfileProgressPartContextFactory
    {
        LocalUserProfileContext CreateLocale(string profileName);
    }
    /// <summary>
    /// Создаем контекст сохранения, для немедленного использования
    /// </summary>
    public class ProfileProgressPartContextFactory : IProfileProgressPartContextFactory, IInitializable
    {
        private readonly IUserSavePathSettings _settings;
        private readonly ITextFileOperation _textFileOperation;

        public ProfileProgressPartContextFactory(ITextFileOperation textFileOperation, IUserSavePathSettings settings)
        {
            _textFileOperation = textFileOperation;
            _settings = settings;
        }

        public LocalUserProfileContext CreateLocale(string profileName)
        {
            var proFilePath = $"{_settings.RootPath}/{profileName}";
            CreateFolder(proFilePath);
            return new LocalUserProfileContext(_textFileOperation, proFilePath);
        }

        public void Initialize()
        {
            CreateFolder(_settings.RootPath);
        }

        void CreateFolder(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }
    }
}
