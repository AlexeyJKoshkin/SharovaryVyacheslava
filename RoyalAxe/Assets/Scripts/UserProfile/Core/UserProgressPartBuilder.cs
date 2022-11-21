namespace Core.UserProfile 
{
    public interface IUserProfileProgressHarvester<out TProgressData> where TProgressData : BaseUserProgressData
    {
        TProgressData GetCurrentSaveData();
    }

    public class MockUserProfileProgressHarvester<TProgressData> : IUserProfileProgressHarvester<TProgressData> where TProgressData : BaseUserProgressData
    {
        public TProgressData GetCurrentSaveData()
        {
            return null;
        }
    }
}