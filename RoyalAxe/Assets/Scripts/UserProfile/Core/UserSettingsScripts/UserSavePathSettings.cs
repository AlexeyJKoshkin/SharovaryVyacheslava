#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Core.UserProfile
{
    [Serializable]
    public abstract class UserSavePathSettings : IUserSavePathSettings
    {
        [field: SerializeField] [FolderPath] public string RootPath { get; protected set; }

        public abstract RuntimePlatform Id { get; }
    }
}