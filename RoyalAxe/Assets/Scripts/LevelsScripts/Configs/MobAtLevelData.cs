using System;

namespace RoyalAxe.CoreLevel {
    [Serializable]
    public class MobAtLevelData
    {
        public string MobId;
        public int Level;
        public byte TotalAmount;

        public MobAtLevelData() { }

        public MobAtLevelData(MobAtLevelData old)
        {
            MobId       = old.MobId;
            Level       = old.Level;
            TotalAmount = old.TotalAmount;
        }
    }
}