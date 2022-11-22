using RoyalAxe.CoreLevel;

namespace Core.UserProfile 
{
    public interface IUserLevelsProgress : IUserProgressProfile
    {
        LastLevel SavedLevel { get; }

        LastLevel LastPlayed { get; }

        void UpdateLastPlayedLevel(LevelGeneratorSettings nextWaveSettings);
    }
}