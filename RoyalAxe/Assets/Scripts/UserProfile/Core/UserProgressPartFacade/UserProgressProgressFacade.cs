using System;

namespace Core.UserProfile
{
    public abstract class UserProfileProgressFacade<TData> :IUserProgressPartFacade where TData : BaseUserProgressData
    {
        private readonly IUserProgressPartFactory<TData> _loader;
        public event Action<IUserProgressPartFacade> OnSaveProgress;
        protected abstract string Key { get; }

        protected TData Progress { get; private set; }

        public UserProfileProgressFacade(IUserProgressPartFactory<TData> loader)
        {
            _loader = loader;
        }

        public void SaveProgress(IProfileProgressStorageContext context)
        {
            var json = _loader.ToJson(Progress);
            context.Save(json,Key);
        }

        public void LoadProgress(IProfileProgressStorageContext context, CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            var json =context.Load(Key);
            Progress =  _loader.LoadTo(json);
            SetToMainFacade(currentGeneralUserProgressProfileFacade);
            UpdateProgressData();
        }

        protected abstract void UpdateProgressData();


        protected void SetDirty()
        {
            OnSaveProgress?.Invoke(this);
        }


        protected abstract void SetToMainFacade(CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade);

        public void Save()
        {
            SetDirty();
        }
    }
}
