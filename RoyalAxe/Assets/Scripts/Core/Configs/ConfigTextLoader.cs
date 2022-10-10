using System;
using System.Threading.Tasks;

namespace Core.Configs
{
    public class ConfigTextLoader : IJsonConfigFileLoader
    {
        private readonly IJsonConfigsPathBuilder _configsPathBuilder;
        private readonly IJsonFileLoader _jsonFileLoader;
        public ConfigTextLoader(IJsonConfigsPathBuilder configsPathBuilder, IJsonFileLoader jsonFileLoader)
        {
            _configsPathBuilder = configsPathBuilder;
            _jsonFileLoader = jsonFileLoader;
        }

        public string LoadText<T>(string path) where T : class
        {
            var fullPath = string.IsNullOrEmpty(path) ? _configsPathBuilder.BuildPathForType<T>() : path;
            var task = _jsonFileLoader.LoadText(fullPath);
            Task.WaitAll(task);
            return task.Result;
        }
    }
}
