using Core.Data.Provider;
using Sirenix.OdinInspector;
using UnityEngine;

//using GameKit.Editor;

namespace Core
{
    public abstract class SOHolderProvider<T> : DataBox<T> where T : ScriptableObject, IDataObject
    {
        [EnumToggleButtons, SerializeField] protected DataBoxProviderType _providerType;
        [FolderPath, SerializeField, EnableIf("@this._providerType.HasFlag(DataBoxProviderType.Folders)")]
        private string[] _foldersPath;
        /*#if UNITY_EDITOR
                public override void Reload()
                {
                    _collection.Clear();
                    _collection.AddRange(Load());
        
                    base.Reload();
                }
        
                private IEnumerable<T> Load()
                {
                    if (_providerType.HasFlag(DataBoxProviderType.SubConfigs))
                    {
                        foreach (var asset in EditorUtils.LoadAllAssetsFrom<T>(this)) yield return asset;
                    }
        
                    if (_providerType.HasFlag(DataBoxProviderType.Folders))
                    {
                        foreach (var path in _foldersPath)
                        {
                            foreach (var asset in EditorUtils.LoadAllAssetsAtPath<T>(path)) yield return asset;
                        }
                    }
                }
        #endif*/
    }
}