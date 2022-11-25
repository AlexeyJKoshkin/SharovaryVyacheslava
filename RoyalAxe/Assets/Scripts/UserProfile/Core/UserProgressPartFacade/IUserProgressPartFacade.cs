using System;

namespace Core.UserProfile
{
    public interface IUserProgressPartFacade
    {
        event Action<IUserProgressPartFacade> OnSaveProgress;
        void SaveProgress(IProfileProgressStorageContext context);
        void LoadProgress(IProfileProgressStorageContext context);
    }

    public abstract class UserProgressPartFacade<TData> : IUserProgressPartFacade, IUserProgressProfile where TData : BaseUserProgressData, new()

    {
        private readonly IUserProgressPartSaveLoader _loader;
        public event Action<IUserProgressPartFacade> OnSaveProgress;
        protected abstract string Key { get; }

        private bool _isSetDirty;


        protected TData Progress { get; private set; }

        public UserProgressPartFacade(IUserProgressPartSaveLoader loader)
        {
            _loader = loader;
        }

        protected void SetDirty()
        {
            if (_isSetDirty)
            {
                return;
            }

            OnSaveProgress?.Invoke(this);
            _isSetDirty = true;
        }

        void IUserProgressPartFacade.SaveProgress(IProfileProgressStorageContext context)
        {
            var json = _loader.ToJson(Progress);
            context.Save(json, Key);
            _isSetDirty = false;
        }

        void IUserProgressPartFacade.LoadProgress(IProfileProgressStorageContext context)
        {
            var json = context.Load(Key);
            Progress = _loader.LoadTo<TData>(json);
        }

        void IUserProgressProfile.Save()
        {
            SetDirty();
        }
    }
}