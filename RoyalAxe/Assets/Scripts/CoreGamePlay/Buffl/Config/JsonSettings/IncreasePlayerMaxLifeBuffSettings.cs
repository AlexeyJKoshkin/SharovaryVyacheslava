using System;
using Core.Parser;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class IncreasePlayerMaxLifeBuffSettings: BaseLevelBuffSettings
    {
        [ColumnName("Percent")]
        //потом решим это будет процент или число
        public int IncreaseValue = 50;
        public IncreasePlayerMaxLifeBuffSettings() : base(LevelBuffType.IncreasePlayerMaxLife) { }
    }
}