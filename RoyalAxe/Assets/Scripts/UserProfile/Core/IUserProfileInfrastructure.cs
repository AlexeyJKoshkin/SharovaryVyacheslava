namespace Core.UserProfile {
    public interface IUserProfileInfrastructure<TData> where TData :BaseUserProgressData
    {
        string FileName { get; }

        TData GetItemToSave(UserProfileData saveobject);

        void SetItemToResult(UserProfileData result, TData item);
    }
}