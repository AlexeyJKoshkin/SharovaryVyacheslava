using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
  

    /// <summary>
    /// Загрузчик кор сцены.
    /// </summary>
    public class CoreGameSceneLoader : ISceneLoaderHelper
    {
        private readonly CoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        private LastLevel _coreLevelParameters;
        private readonly IDataStorage _dataStorage;
        public GameSceneType TargetScene => GameSceneType.Core;

        public CoreGameSceneLoader(CoreLevelDataInfrastructure coreLevelDataInfrastructure, IDataStorage dataStorage)
        {
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            _dataStorage = dataStorage;
        }

        public void SetPlayerParameters(LastLevel coreLevelParams)
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
            var biome = _dataStorage.ById<BiomeScriptableDef>(_coreLevelParameters.Biome.ToString());
            _coreLevelDataInfrastructure.BiomeDef = biome;
            _coreLevelDataInfrastructure.LevelNumber = _coreLevelParameters.LevelNumber;
           
            var allLevels = _dataStorage.All<LevelSettingsData>() // Уровни
                                        .Where(o=> o.LevelNumber >= _coreLevelParameters.LevelNumber) // которые больше текущего
                                        .ToList();



            _coreLevelDataInfrastructure.PackLevels = allLevels;
            // подгружаем ресурсы : биом
            return Task.CompletedTask;
        }
    }
}
