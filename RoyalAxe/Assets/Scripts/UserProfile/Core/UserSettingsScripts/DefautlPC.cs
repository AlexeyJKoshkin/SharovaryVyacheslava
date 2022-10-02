using System;
using UnityEngine;

namespace Core.UserProfile
{
    [Serializable]
    public class DefautlPC : UserSavePathSettings
    {
        public DefautlPC()
        {
            RootPath = "Assets/Temp/[Editor_Save_folder]/";
        }

        public override RuntimePlatform Id => RuntimePlatform.WindowsEditor;
    }
}