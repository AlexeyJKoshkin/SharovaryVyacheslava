using System;

namespace Core.Configs
{
    public interface IJsonConfigsPathBuilder
    {
        string BuildForType<T>(string extension = "json");
        string BuildForType(Type type, string extension = "json");
    }

    public class JsonConfigsPathBuilder : IJsonConfigsPathBuilder
    {
        private string EditorPath = "Assets/Data/Configs/Json";

        public string BuildForType<T>(string extension = "json")
        {
            return BuildForType(typeof(T));
        }

        public string BuildForType(Type type, string extension = "json")
        {
            return $"{GetRootFolderConfigPath()}/{type.Name}.{extension}";
        }

        private string GetRootFolderConfigPath()
        {
#if UNITY_EDITOR
            return EditorPath;
#endif
            //todo: реализовать правильный путь на устройстве
        }
    }
}