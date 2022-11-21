namespace Core.UserProfile
{
    public interface IUserProfileProgressEntityBuilder<in TProgressData> where TProgressData : BaseUserProgressData
    {
        IUserProgressProfile BuildGameEntity(TProgressData progressData);
    }
}
