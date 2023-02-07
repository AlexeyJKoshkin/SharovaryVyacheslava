using System.Collections.Generic;
using System.Linq;
using Core.Configs;

using UnityEngine;

namespace RoyalAxe.Configs
{
    public class BuildInJsonConfigLoader : IJsonConfigFileLoader
    {
        private Dictionary<string, TextAsset> _assetsByTypeName = new Dictionary<string, TextAsset>();
        
        public BuildInJsonConfigLoader(BuildInJsonDataProvider buildInJsonDataProvider)
        {
            _assetsByTypeName = buildInJsonDataProvider.Assets().ToDictionary(e => e.name, e => e);
        }

        public string LoadText<T>(string path) where T : class
        {
            var fileName = typeof(T).Name;

            if (_assetsByTypeName.TryGetValue(fileName, out var textAsset)) return textAsset.text;
            return null;
        }
    }
}
