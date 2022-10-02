namespace Core.Configs
{
    public interface ITextFileOperation : IJsonFileLoader
    {
        void Save(string path, string json);
    }
}