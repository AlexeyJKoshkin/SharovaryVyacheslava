using System;

namespace Core.UserProfile 
{
    public class UserProgressLoader<TData> where TData : BaseUserProgressData
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IDefaultProgressFactory<TData> _defaultProgressFactory;
        private readonly IUserProfileInfrastructure<TData> _userProfileInfrastructure;
        public UserProgressLoader(IJsonConverter jsonConverter,
                                  IDefaultProgressFactory<TData> defaultProgressFactory,
                                  IUserProfileInfrastructure<TData> userProfileInfrastructure,
                                  IProfileProgressStorageContext profileContext)
        {
            _jsonConverter          = jsonConverter;
            _defaultProgressFactory = defaultProgressFactory;
            _userProfileInfrastructure = userProfileInfrastructure;
        }

        public string GetSaveString(UserProfileData saveobject)
        {
            var item = _userProfileInfrastructure.GetItemToSave(saveobject);
            return  _jsonConverter.SerializeObject(item);
            //_currentProfileContext.Save(json, _userSaveProfileAdapter.FileName);
        }

        public void LoadTo(UserProfileData result, string json)
        {
            //string json = _currentProfileContext.Load(_userSaveProfileAdapter.FileName);
            var    item =  Parse(json) ?? _defaultProgressFactory.CreateDefault();
            _userProfileInfrastructure.SetItemToResult(result, item);
        }
        
        private TData Parse(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

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
    }

    public interface IUserProgressProfileFacade : IUserProfilePartBuilder<UserProfileData>
    {
        
    }

    public class UserProgressProfileFacade<TData> :IUserProgressProfileFacade where TData : BaseUserProgressData
    {
        private readonly UserProgressLoader<TData> _loader;
        private readonly IUserProfileInfrastructure<TData> _userProfileInfrastructure;
        public UserProgressProfileFacade(UserProgressLoader<TData> loader, IUserProfileInfrastructure<TData> userProfileInfrastructure)
        {
            _loader = loader;
            _userProfileInfrastructure = userProfileInfrastructure;
        }

        public void SaveProgress(IProfileProgressStorageContext context,UserProfileData saveobject)
        {
            var json = _loader.GetSaveString(saveobject);
            context.Save(json,_userProfileInfrastructure.FileName);
        }

        public void LoadTo(IProfileProgressStorageContext context, UserProfileData result)
        {
            var json =context.Load(_userProfileInfrastructure.FileName);
            _loader.LoadTo(result,json);
        }
    }

   
}