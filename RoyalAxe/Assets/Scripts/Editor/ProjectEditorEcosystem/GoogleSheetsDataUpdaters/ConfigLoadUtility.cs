using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;
using GameKit.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    [Serializable]
    internal class ConfigLoadUtility
    {
        [SerializeField] private DataBox _dataBox;

        [SerializeField] private DefaultAsset _rootFolderConfigs;

        [ShowIf("_dataBox", null), SerializeField]
        private bool CreateNew;
        [ShowIf("_dataBox", null), SerializeField]
        private bool IncludeSubFolders;
        public string RootPath => _rootFolderConfigs == null ? null : AssetDatabase.GetAssetPath(_rootFolderConfigs);

        public TConfig GetById<TConfig>(string id, string folder) where TConfig : ScriptableObject, IDataObject
        {
            var fullName = $"{folder}/{id}.asset";
            return EditorUtils.LoadAsset<TConfig>(fullName, CreateNew);
        }

        public TConfig GetById<TConfig>(string id) where TConfig : ScriptableObject, IDataObject
        {
            if (_rootFolderConfigs == null) return null;
     

            var path = AssetDatabase.GetAssetPath(_rootFolderConfigs);
            return GetById<TConfig>(id, path);
        }

        public void UpdateDataBox(string folder)
        {
            var allInFolder = EditorUtils.LoadAllAssetsAtPath<ScriptableObject>(folder, IncludeSubFolders)
                                         .Where(o => o is IDataObject)
                                         .Cast<Object>()
                                         .ToList();
            var          ser  = new SerializedObject(_dataBox);
            var          prop = ser.FindProperty("_collection");
            List<Object> all  = LoadItems(prop).ToList();
            allInFolder.Where(o => !all.Contains(o))
                       .ForEach(e => all.Add(e));
            SaveToConfig(prop, all);
            ser.ApplyModifiedProperties();
            EditorUtility.SetDirty(_dataBox);
        }

        private void SaveToConfig(SerializedProperty prop, List<Object> all)
        {
            prop.ClearArray();
            for (int i = 0; i < all.Count; i++)
            {
                prop.InsertArrayElementAtIndex(i);
                prop.GetArrayElementAtIndex(i).objectReferenceValue = all[i];
            }
        }

        private IEnumerable<Object> LoadItems(SerializedProperty prop)
        {
            for (int i = 0; i < prop.arraySize; i++)
            {
                var item = prop.GetArrayElementAtIndex(i);
                if (item.objectReferenceValue == null)
                {
                    continue;
                }

                if (item.objectReferenceValue is IDataObject)
                {
                    yield return item.objectReferenceValue;
                }
            }
        }

        public void UpdateDataBox()
        {
            if (_rootFolderConfigs == null) return;
            var path = AssetDatabase.GetAssetPath(_rootFolderConfigs);
            UpdateDataBox(path);
        }
    }
}