using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.Configs
{
    public class BuildInJsonDataProvider : ScriptableObject
    {
        [SerializeField]
        private TextAsset[] _allAssets;
        
        public IEnumerable<TextAsset> Assets()
        {
            return _allAssets;
        }
        
        #if UNITY_EDITOR
        [SerializeField]
        private UnityEditor.DefaultAsset _folder;

        [Button, EnableIf("_folder")]
        void LoadAll()
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(_folder);
            _allAssets = GameKit.Editor.EditorUtils.LoadAllAssetsAtPath<TextAsset>(path).ToArray();
        }
#endif
    }
}
