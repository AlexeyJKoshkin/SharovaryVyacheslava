using System;
using Newtonsoft.Json;

namespace Core
{
    public class SVJsonConverter : IJsonConverter
    {
        protected readonly JsonSerializerSettings _jsonSettings;


        public SVJsonConverter()
        {
            _jsonSettings = new JsonSerializerSettings
            {
                Formatting       = Formatting.None,
                ContractResolver = new EnumToIntKeyContractResolver()
            };
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, _jsonSettings);
        }

        public string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _jsonSettings);
        }

        public T Deserialize<T>(string containerJsonString)
        {
            return JsonConvert.DeserializeObject<T>(containerJsonString, _jsonSettings);
        }
    }
}