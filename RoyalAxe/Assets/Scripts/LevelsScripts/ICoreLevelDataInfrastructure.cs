using System.Collections.Generic;

namespace RoyalAxe.CoreLevel 
{
    //Инфраструктура с данными для работы текущего уровня
    public interface ICoreLevelDataInfrastructure
    {
        IReadOnlyList<LevelGeneratorSettings> PackLevels { get; }

        int LevelNumber { get; }

        BiomeScriptableDef BiomeDef { get; }
    }

    public class CoreLevelDataInfrastructure : ICoreLevelDataInfrastructure
    {
        public IReadOnlyList<LevelGeneratorSettings> PackLevels { get; set; }
        public int LevelNumber { get; set; }
        public BiomeScriptableDef BiomeDef { get; set; }

        public override string ToString()
        {
            return $"{BiomeDef?.UniqueID} -> {LevelNumber} Pack {PackLevels?.Count}";
        }

        /*public CoreLevelDataInfrastructure(List<LevelGeneratorSettings> levels, BiomeScriptableDef biomeDef)
        {
            PackLevels = levels;
            BiomeDef = biomeDef;
            BiomeType = levels[0].Type;
        }*/
    }
}