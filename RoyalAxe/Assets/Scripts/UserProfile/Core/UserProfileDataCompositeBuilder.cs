using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VContainer.Unity;

namespace Core.UserProfile
{
    public interface IUserProgressProfileBuilderComposite
    {
        void Load(UserProfileData result);
        void Save(UserProfileData profile);
    }

    public class UserProgressProfileBuilderComposite : IUserProgressProfileBuilderComposite
    {
        private readonly IProfileProgressPartContextFactory _factory;
        private readonly IReadOnlyList<IUserProgressProfileFacade> _builders;
        public UserProgressProfileBuilderComposite(IProfileProgressPartContextFactory factory, IReadOnlyList<IUserProgressProfileFacade> builders)
        {
            _factory = factory;
            _builders = builders;
        }

        public void Load(UserProfileData profile)
        {
            var context = _factory.CreateLocale(profile); // Создаем текущий контекст для загрузки профиля
            foreach (var builder in _builders)
            {
                builder.LoadTo(context,profile);
            }
        }

        public void Save(UserProfileData profile)
        {
            var context = _factory.CreateLocale(profile); // Создаем текущий контекст для загрузки профиля
            foreach (var builder in _builders)
            {
                builder.SaveProgress(context,profile);
            }
        }
    }

    public class UserProfileDataCompositeBuilder : IUserSaveProfileCRUDCommand<UserProfileData>, IInitializable
    {
        private readonly IUserProgressProfileBuilderComposite _builder;
        private readonly IUserSavePathSettings _savePathSettings;

        public UserProfileDataCompositeBuilder(IUserSavePathSettings savePathSettings,
                                                IUserProgressProfileBuilderComposite builder)
        {
            _savePathSettings = savePathSettings;
            _builder = builder;
        }

        public UserProfileData Create(string profileName)
        {
            string path = $"{_savePathSettings.RootPath}{profileName}";
            Directory.CreateDirectory(path);
            return Read(profileName);
        }

        public UserProfileData Read(string profileName)
        {
            var result = new UserProfileData() {ProfileName = profileName};
            _builder.Load(result);
            return result;
        }

        public void Update(UserProfileData profile)
        {
            string path = $"{_savePathSettings.RootPath}{profile.ProfileName}";
            if(!Directory.Exists(path)) return;
            _builder.Save(profile);
        }

        public void Delete(string profileName)
        {
            string path = $"{_savePathSettings.RootPath}{profileName}";
            Directory.Delete(path);
        }

        public void Initialize()
        {
            if(Directory.Exists(_savePathSettings.RootPath)) return;
            Directory.CreateDirectory(_savePathSettings.RootPath);
        }
    }
}