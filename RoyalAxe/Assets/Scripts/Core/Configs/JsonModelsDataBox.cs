using System;
using System.Collections.Generic;
using Core.Data.Provider;

namespace Core.Configs
{
    public class JsonModelsDataBox<T> : IDataBox<T> where T : class, IDataObject
    {
        public IReadOnlyCollection<T> Collection => _collection.Values;
        public T this[int id] => _collection.TryGetValue(id, out var result) ? result : null;
        
        private readonly Dictionary<int, T> _collection = new Dictionary<int, T>();
        
        private readonly IJsonConfigsModelsLoader _configsModelsLoader;

        public JsonModelsDataBox(IJsonConfigsModelsLoader configsModelsLoader)
        {
            _configsModelsLoader = configsModelsLoader;
        }

        public Type ObjectType => typeof(T);
        public int Count => _collection.Count;

        public void Reload()
        {
            _collection.Clear();
            if (_configsModelsLoader == null)    return;
            foreach (var model in _configsModelsLoader.Load<T>()) _collection.Add(model.UniqueID.GetHashCode(), model);
        }


    }
}