using System;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreasePlayerMaxLifeBuffSettings: BaseLevelBuffSettings
    {
        //потом решим это будет процент или число
        public int IncreaseValue = 50;
        public IncreasePlayerMaxLifeBuffSettings() : base(LevelBuffType.IncreasePlayerMaxLife) { }
    }
}