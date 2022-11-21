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
        
        //имя файла в котором хранится прогресс
        //public abstract string FileName { get; }
    }
}