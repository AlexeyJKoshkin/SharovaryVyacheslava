using System;

namespace RoyalAxe.LevelBuff
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LevelAdditionSettingsAttribute : Attribute
    {
        public bool IsSingle;

        public LevelAdditionSettingsAttribute(bool isSingle = false)
        {
            IsSingle = isSingle;
        }
    }
}