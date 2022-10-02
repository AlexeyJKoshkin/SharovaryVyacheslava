using System;
using RoyalAxe.Utility;

namespace RoyalAxe.CoreLevel {
    [Serializable]
    public class LineModel
    {
        public int Size;
        public bool AnyMob => MobId.Length == 0;
        [MobId]
        public string[] MobId;
    }
}