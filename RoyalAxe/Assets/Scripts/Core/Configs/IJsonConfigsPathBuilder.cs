using System;
using UnityEngine;

namespace Core.Configs
{
    public interface IJsonConfigsPathBuilder
    {
        string BuildForType<T>(string extension = "json");
        string BuildForType(Type type, string extension = "json");
    }

    public class JsonConfigsPathBuilder : IJsonConfigsPathBuilder
    {
        public string ConfigsJsonConfigsPath = "Configs/Json";

        private readonly string _fullPath;

        public JsonConfigsPathBuilder()
        {
            _fullPath = GetRootFolderConfigPath();
        }

        public string BuildForType<T>(string extension = "json")
        {
            return BuildForType(typeof(T));
        }

        public string BuildForType(Type type, string extension = "json")
        {
            return $"{_fullPath}/{type.Name}.{extension}";
        }

        private string GetRootFolderConfigPath()
        {
            string rootPath;
#if UNITY_EDITOR
            rootPath = "Assets/Data";
#else
    rootPath = Application.persistentDataPath;
#endif
            return $"{rootPath}/{ConfigsJsonConfigsPath}";
        }
    }
}