using System;
using RoyalAxe.CoreLevel;

namespace Core.UserProfile
{
    [Serializable]
    public class UserLevelProgress : BaseUserProgressData
    {
        public LastLevel LastSavedLevel = LastLevel.Default;
        public LastLevel LastPlayedLevel = LastLevel.Default;
        // можно сохранять стату по уровням. или еще какой-то другой прогресс по уровню
        private object LevelsProgressInfo = null;
        //    public override string FileName => "Levels";
    }

    [Serializable]
    public struct LastLevel
    {
        public static LastLevel Default = new LastLevel {Biome = BiomeType.Forest, LevelNumber = 1};
        public BiomeType Biome;
        public int LevelNumber;
    }
}