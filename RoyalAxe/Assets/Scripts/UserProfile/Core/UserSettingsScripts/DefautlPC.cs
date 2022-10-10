using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.UserProfile
{
    [Serializable]
    public class DefautlPC : UserSavePathSettings
    {
        public override string RootPath => _rootPath;
        [field: SerializeField] [FolderPath]
        protected string _rootPath;
        public DefautlPC()
        {
            _rootPath = "Assets/Temp/[Editor_Save_folder]/";
        }

        public override RuntimePlatform Id => RuntimePlatform.WindowsEditor;
    }
}