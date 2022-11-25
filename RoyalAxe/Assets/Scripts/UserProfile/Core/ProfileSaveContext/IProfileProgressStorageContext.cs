namespace Core.UserProfile
{
    public interface IProfileProgressStorageContext
    {
        void Save(string json, string key);
        string Load(string key);
    }
}