using System.Collections.Generic;

namespace RoyalAxe.CoreLevel 
{
    public interface ICoreLevelDataInfrastructure
    {
        BiomeType BiomeType { get; }
        IReadOnlyList<LevelGeneratorSettings> PackLevels { get; }
    }

    public class CoreLevelDataInfrastructure : ICoreLevelDataInfrastructure
    {
        public BiomeType BiomeType { get; }
        public IReadOnlyList<LevelGeneratorSettings> PackLevels { get; }

        public CoreLevelDataInfrastructure(List<LevelGeneratorSettings> levels)
        {
            PackLevels = levels;
            BiomeType = levels[0].Type;
        }
    }
}