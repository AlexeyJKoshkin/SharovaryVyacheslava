using System;

namespace Core.UserProfile
{
    public interface IUserProgressPartFacade : IUserProgressProfile
    {
        void SaveProgress(IProfileProgressStorageContext context);
        void LoadProgress(IProfileProgressStorageContext context, CurrentGeneralUserProgressProfileFacade currentGeneralUserProgressProfileFacade);
        event Action<IUserProgressPartFacade> OnSaveProgress;
    }
}
