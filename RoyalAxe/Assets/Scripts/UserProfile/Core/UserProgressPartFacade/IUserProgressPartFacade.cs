namespace Core.UserProfile
{
    public interface IUserProgressPartFacade
    {
        void SaveProgress(IProfileProgressStorageContext context);
        void LoadProgress(IProfileProgressStorageContext context, IUserProfileProgressRoot currentUserProgressProfileFacade);
    }
}
