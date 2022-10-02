using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Data.Provider;

namespace Core.Configs
{
    public class JsonConfigsModelOperation : IJsonConfigModelsOperation
    {
        private readonly IJsonConfigsPathBuilder _configsPathBuilder;
        private readonly IJsonFileLoader _jsonFileLoader;
        private readonly IJsonConverter _jsonConverter;

        public JsonConfigsModelOperation(IJsonConfigsPathBuilder configsPathBuilder, IJsonFileLoader jsonFileLoader, IJsonConverter jsonConverter)
        {
            _configsPathBuilder = configsPathBuilder;
            _jsonFileLoader     = jsonFileLoader;
            _jsonConverter      = jsonConverter;
        }

        public IEnumerable<T> Load<T>(string path = null) where T : class
        {
            var task = LoadJson<T>(path);
            Task.WaitAll(task);
            var json = task.Result;
            if (string.IsNullOrEmpty(json))
            {
                return new T[0];
            }

            return _jsonConverter.Deserialize<T[]>(json);
        }

        public T LoadSingle<T>(string path = null) where T : class
        {
            var task = LoadJson<T>(path);
            Task.WaitAll(task);
            var json = task.Result;
            return string.IsNullOrEmpty(json) ? default : _jsonConverter.Deserialize<T>(json);
        }
        
        private Task<string> LoadJson<T>(string path) where T : class
        {
            var fullPath = string.IsNullOrEmpty(path) ? _configsPathBuilder.BuildForType<T>() : path;
            return _jsonFileLoader.LoadText(fullPath);
        }

        void IJsonConfigModelsOperation.Save<T>(IEnumerable<T> data)
        {
            var json = data == null ? "[]" : _jsonConverter.SerializeObject(data);
            SaveTo<T>(json);
        }

        private void SaveTo<T>(string json) where T : class, IDataObject
        {
            SaveTo(typeof(T), json);
        }

        private void SaveTo(Type type, string json)
        {
            var fullPath = _configsPathBuilder.BuildForType(type);
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

        public void Save<T>(T data) where T : class, IDataObject
        {
            var json = data == null ? "{}" : _jsonConverter.SerializeObject(data);
            SaveTo<T>(json);
        }

        public void Save(object data)
        {
            string json = null;
            if (data is IDataObject)
            {
                json = _jsonConverter.SerializeObject(data);
            }

            if (data is IEnumerable)
            {
                json = _jsonConverter.SerializeObject(data);
            }

            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            SaveTo(data.GetType(), json);
        }
    }
}