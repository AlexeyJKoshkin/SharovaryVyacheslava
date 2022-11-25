using System;

namespace Core.UserProfile
{
    public interface IUserProgressSaveLoader : IUserProgressProfile
    {
        void SaveProgress(IProfileProgressStorageContext context);
        void LoadProgress(IProfileProgressStorageContext context, GeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade);
        event Action<IUserProgressSaveLoader> OnSaveProgress;
    }

    public interface IIUserProgressAdapter<TData> :IUserProgressProfile where TData : BaseUserProgressData
    {
        TData Progress { get; }
    }

    public interface IUserProgressPartFacade
    {
        void InitTo(GeneralUserProgressProfileFacade result);
    }

    public abstract class UserProgressPartFacade<TData> :IUserProgressPartFacade  where TData : BaseUserProgressData
    {
        private readonly IIUserProgressAdapter<TData> _progressAdapter;

        protected TData Progress => _progressAdapter.Progress;

        public UserProgressPartFacade(IIUserProgressAdapter<TData> progressAdapter)
        {
            _progressAdapter = progressAdapter;
        }
        
        protected void SetDirty()
        {
            _progressAdapter?.Save();
        }

        public abstract void InitTo(GeneralUserProgressProfileFacade result);

        
    }
}
