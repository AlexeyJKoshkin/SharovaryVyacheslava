using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Core.Data.Provider;

namespace Core.Configs
{
    public class JsonConfigsModelOperation : IJsonConfigModelsOperation
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IJsonConfigFileLoader _jsonConfigFileLoader;
        private readonly IJsonConfigsPathBuilder _jsonConfigsPathBuilder;

        public JsonConfigsModelOperation(IJsonConverter jsonConverter, IJsonConfigFileLoader jsonConfigFileLoader, IJsonConfigsPathBuilder jsonConfigsPathBuilder)
        {
            _jsonConverter      = jsonConverter;
            _jsonConfigFileLoader = jsonConfigFileLoader;
            _jsonConfigsPathBuilder = jsonConfigsPathBuilder;
        }

        public IEnumerable<T> Load<T>(string path = null) where T : class
        {
            var json = _jsonConfigFileLoader.LoadText<T>(path);
            if (string.IsNullOrEmpty(json))
            {
                return new T[0];
            }

            return _jsonConverter.Deserialize<T[]>(json);
        }

        public T LoadSingle<T>(string path = null) where T : class
        {
            var json = _jsonConfigFileLoader.LoadText<T>(path);
            return string.IsNullOrEmpty(json) ? default : _jsonConverter.Deserialize<T>(json);
        }
        

        void IJsonConfigModelsOperation.Save<T>(IEnumerable<T> data)
        {
            var json = data == null ? "[]" : _jsonConverter.SerializeObject(data);
            SaveTo<T>(json);
        }

        public void Save<T>(T data)
        {
            var json = data == null ? "{}" : _jsonConverter.SerializeObject(data);
            SaveTo<T>(json);
        }

        private void SaveTo<T>(string json)
        {
            SaveTo(typeof(T), json);
        }

        private void SaveTo(Type type, string json)
        {
            var fullPath = _jsonConfigsPathBuilder.BuildPathForType(type);
            if (!File.Exists(fullPath))
            {
                using (var file = File.CreateText(fullPath))
                {
                    file.WriteLine(json);
                }
            }
            else
            {
                File.WriteAllText(fullPath, json);
            }
        }
    }
}