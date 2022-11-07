using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Data.Provider;
using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    [Serializable]
    public class CoreLevelParameters
    {
        public BiomeType BiomeType;
        public int StartLevel;
    }

    /// <summary>
    /// Загрузчик кор сцены.
    /// </summary>
    public class CoreGameSceneLoader : ISceneLoaderHelper
    {
        private readonly CoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        private CoreLevelParameters _coreLevelParameters;
        private readonly IDataStorage _dataStorage;
        public GameSceneType TargetScene => GameSceneType.Core;

        public CoreGameSceneLoader(CoreLevelDataInfrastructure coreLevelDataInfrastructure, IDataStorage dataStorage)
        {
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            _dataStorage = dataStorage;
        }

        public void SetPlayerParameters(CoreLevelParameters coreLevelParams)
        {
            _coreLevelParameters = coreLevelParams;
        }

        public Task UnloadResources()
        {
            _coreLevelDataInfrastructure.BiomeDef = null;
            _coreLevelDataInfrastructure.PackLevels = null;
            return Task.CompletedTask;
        }

        public Task PreloadResources()
        {
            var biome = _dataStorage.ById<BiomeScriptableDef>(_coreLevelParameters.BiomeType.ToString());
            _coreLevelDataInfrastructure.BiomeDef = biome;
            _coreLevelDataInfrastructure.LevelNumber = _coreLevelParameters.StartLevel;
           
            var allLevels = _dataStorage.All<LevelGeneratorSettings>() // Уровни
                                        .Where(o=> o.LevelNumber >= _coreLevelParameters.StartLevel) // которые больше текущего
                                        .ToList();



            _coreLevelDataInfrastructure.PackLevels = allLevels;
            // подгружаем ресурсы : биом
            return Task.CompletedTask;
        }
    }
}
