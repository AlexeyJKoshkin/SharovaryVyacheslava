using System;

namespace Core.UserProfile
{
    public abstract class UserProfileProgressSaveLoaderAdapter<TData> :IIUserProgressAdapter<TData>,IUserProgressSaveLoader where TData : BaseUserProgressData
    {
        private readonly IUserProgressPartFactory<TData> _loader;
        public event Action<IUserProgressSaveLoader> OnSaveProgress;
        protected abstract string Key { get; }

        public TData Progress { get; private set; }

        public UserProfileProgressSaveLoaderAdapter(IUserProgressPartFactory<TData> loader)
        {
            _loader = loader;
        }

        public void SaveProgress(IProfileProgressStorageContext context)
        {
            var json = _loader.ToJson(Progress);
            context.Save(json,Key);
        }

        public void LoadProgress(IProfileProgressStorageContext context, GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade)
        {
            var json =context.Load(Key);
            Progress =  _loader.LoadTo(json);
            SetToMainFacade(currentGeneralUserProgressProfileFacade);
            UpdateProgressData();
        }

        protected virtual void UpdateProgressData() { }



        protected void SetDirty()
        {
            OnSaveProgress?.Invoke(this);
        }


        protected abstract void SetToMainFacade(GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade);

        public void Save()
        {
            SetDirty();
        }
    }
}
