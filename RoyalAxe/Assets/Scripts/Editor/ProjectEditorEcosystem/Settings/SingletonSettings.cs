#region

using System;
using System.Collections.Generic;
using GameKit.Editor;
using UnityEngine;

#endregion

namespace ProjectEditorEcoSystem
{
    public class SingletonSettings<T> : ScriptableObject where T : SingletonSettings<T>
    {
        public const string ROOT_FOLDER_NAME = "Assets/SharovaruVaycheslava/";
        private static Dictionary<Type, string> _folderNames = new Dictionary<Type, string>();

        private static T _instance;
        public static T Instance
        {
            get
            {
                _instance = EditorUtils.FindAsset<T>();
                if (_instance == null)
                {
                    _instance = CreateSettings<T>();
                }

                return _instance;
            }
        }

        private static TSingletonSettings CreateSettings<TSingletonSettings>() where TSingletonSettings : SingletonSettings<TSingletonSettings>
        {
            EditorUtils.CreateAssetFolder(ROOT_FOLDER_NAME);

            string folderName = "";
            Type   type       = typeof(T);
            string path       = null;
            if (_folderNames.TryGetValue(type, out folderName))
            {
                string folderPath = ROOT_FOLDER_NAME + folderName;
                EditorUtils.CreateAssetFolder(folderPath);
                path = folderPath + type.Name + ".asset";
            }
            else
            {
                path = ROOT_FOLDER_NAME + type.Name + ".asset";
            }

            return EditorUtils.CreateAsset<TSingletonSettings>(path);
        }
    }
}