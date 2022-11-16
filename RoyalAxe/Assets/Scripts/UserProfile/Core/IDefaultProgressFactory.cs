namespace Core.UserProfile
{
    public interface IDefaultProgressFactory<TData> where TData : BaseUserProgressData
    {
        TData CreateDefault();
    }
}
