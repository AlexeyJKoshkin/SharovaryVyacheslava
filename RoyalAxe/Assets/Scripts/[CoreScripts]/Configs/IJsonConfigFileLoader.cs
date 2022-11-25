namespace Core.Configs
{
    public interface IJsonConfigFileLoader
    {
        string LoadText<T>(string path) where T : class;
        
    }
}
