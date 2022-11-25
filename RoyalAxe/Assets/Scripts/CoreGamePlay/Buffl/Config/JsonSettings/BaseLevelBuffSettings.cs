using System;

namespace RoyalAxe.LevelBuff 
{
    [Serializable]
    public abstract class BaseLevelBuffSettings
    {
        public LevelBuffType Type;

        public BaseLevelBuffSettings(LevelBuffType type)
        {
            Type = type;
        }
    }
}