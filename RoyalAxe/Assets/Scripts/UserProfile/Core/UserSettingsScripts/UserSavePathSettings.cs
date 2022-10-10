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
        public abstract string RootPath { get; }


        public abstract RuntimePlatform Id { get; }
    }
}