#region

using System;


#endregion

namespace Core.UserProfile
{
    [Serializable]
    public abstract class BaseUserProgressData
    {
        //версия структуры
        public int FormatVersion { get; set; }
    }

    [Serializable]
    public struct SaveEntityRecord
    {
        public string Id;
        public int Level;
    }
}