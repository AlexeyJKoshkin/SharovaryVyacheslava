using Core.Configs;

namespace Core.UserProfile
{
    //Контекстное хранилище текущего профиля

    public class LocalUserProfileContext : IProfileProgressStorageContext
    {
        private readonly ITextFileOperation _textFileOperation;
        private readonly string _rootProfileDirectory;

        public LocalUserProfileContext(ITextFileOperation textFileOperation, string rootProfileDirectory)
        {
            _textFileOperation = textFileOperation;
            _rootProfileDirectory = rootProfileDirectory;
        }

        void IProfileProgressStorageContext.Save(string json, string key)
        {
            string pathToFile = $"{_rootProfileDirectory}/{key}.json";
            _textFileOperation.Save(pathToFile, json);
        }

        string IProfileProgressStorageContext.Load(string key)
        {
            string pathToFile = $"{_rootProfileDirectory}/{key}.json";
            return _textFileOperation.LoadText(pathToFile).Result;
        }
    }
}
