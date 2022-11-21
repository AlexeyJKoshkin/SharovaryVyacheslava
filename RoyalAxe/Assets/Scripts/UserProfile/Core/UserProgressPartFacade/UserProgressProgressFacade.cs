namespace Core.UserProfile
{
    public abstract class UserProfileProgressFacade<TData> :IUserProgressPartFacade where TData : BaseUserProgressData
    {
        private readonly IUserProgressPartFactory<TData> _loader;
        private readonly IUserProfileProgressEntityBuilder<TData> _userProfileProgressEntityBuilder;
        private readonly IUserProfileProgressHarvester<TData> _harvester;
        protected abstract string Key { get; }

        public UserProfileProgressFacade(IUserProgressPartFactory<TData> loader,  IUserProfileProgressEntityBuilder<TData> userProfileProgressEntityBuilder, IUserProfileProgressHarvester<TData> harvester)
        {
            _loader = loader;
            _userProfileProgressEntityBuilder = userProfileProgressEntityBuilder;
            _harvester = harvester;
        }

        public void SaveProgress(IProfileProgressStorageContext context)
        {
            var data = _harvester.GetCurrentSaveData();
            var json = _loader.ToJson(data);
            context.Save(json,Key);
        }

        public void LoadProgress(IProfileProgressStorageContext context, IUserProfileProgressRoot currentUserProgressProfileFacade)
        {
            var json =context.Load(Key);
            var progressData =  _loader.LoadTo(json);
            IUserProgressProfile progress =  _userProfileProgressEntityBuilder.BuildGameEntity(progressData);
           currentUserProgressProfileFacade.AddPartProgress(progress);
        }
    }
}
