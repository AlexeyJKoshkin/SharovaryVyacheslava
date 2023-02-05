using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using GameKit;
using RoyalAxe.GameEntitas;
using RoyalAxe.LevelSkill;

namespace RoyalAxe.CoreLevel
{
    public class WaveLevelSwitcher : ILevelWaveLoader
    {
        public bool HasWave => _waveQueue.Count > 0;
        public int WaveNumber => _waveEntity.levelNumber.Number;

        readonly Queue<LevelSettingsData> _waveQueue = new Queue<LevelSettingsData>();

        private CoreGamePlayEntity _waveEntity => _coreGamePlayContext.levelWaveEntity;


        private readonly CoreGamePlayContext _coreGamePlayContext;
        private readonly IDataStorage _dataStorage;

        private readonly IMobBlueprintsSpawnStorage _mobBlueprintsSpawnStorage;


        public WaveLevelSwitcher(CoreGamePlayContext coreGamePlayContext,
                                 IDataStorage dataStorage,
                                 IMobBlueprintsSpawnStorage mobBlueprintsSpawnStorage)
        {
            _coreGamePlayContext       = coreGamePlayContext;
            _dataStorage               = dataStorage;
            _mobBlueprintsSpawnStorage = mobBlueprintsSpawnStorage;
            _coreGamePlayContext.isLevelWave = true;
            _coreGamePlayContext.levelWaveEntity.AddLevelMobBluePrints(new List<GenerateMobBlueprintCounter>());
        }

        public bool NextWave()
        {
            if (_waveQueue.Count == 0) return false;

            int nextWave         = WaveNumber + 1;
            var nextWaveSettings = _waveQueue.Dequeue();

            SetNewWave(nextWaveSettings);
            _waveEntity.ReplaceLevelWaveQueue(_waveQueue);
            _waveEntity.ReplaceLevelNumber(nextWave);
            return false;
        }

        public void Init(ICoreLevelDataInfrastructure levelData)
        {
            _waveQueue.Clear();
            levelData.PackLevels.ForEach(e => _waveQueue.Enqueue(e));
            _waveEntity.ReplaceLevelWaveQueue(_waveQueue);
            _mobBlueprintsSpawnStorage.PrepareBlueprints(levelData.PackLevels.SelectMany(o => o.MobsData));

            _waveEntity.AddLevelNumber(0);
        }

        private void SetNewWave(LevelSettingsData nextWaveSettingsData)
        {
            var totalMobs = nextWaveSettingsData.MobsData.Sum(o => o.TotalAmount);
            var pack = _mobBlueprintsSpawnStorage.CreateWavePack(nextWaveSettingsData.MobsData);
            _waveEntity.levelMobBluePrints.Collection.AddRange(pack); // подготовили следующую пачку мобов
            _waveEntity.isWaveFinished = false;

            HandleDestiny(nextWaveSettingsData.Destiny);

            _waveEntity.ReplaceCurrentLevelInfo(new LastLevel()
            {
                LevelNumber = nextWaveSettingsData.LevelNumber,
                Biome       = nextWaveSettingsData.Type
            });
        }

        private void HandleDestiny(WaveDestiny destiny) // todo: корявый метод. По хорошему надо как-то обрабатывать разные ID. 
        {
            if (destiny.HasDestiny)
            {
                var wizardData = GetWizardShop(destiny);

                if (wizardData != null) // если установлен какой-то магазин
                {
                    _waveEntity.ReplaceWizardShopReady(wizardData.PossibleBuffs);
                    // _waveEntity.isWaveMobReady = false;
                    return;
                }
            }

            if (_waveEntity.hasWizardShopReady) _waveEntity.RemoveWizardShopReady();
        }

        private WizardShopSettings GetWizardShop(WaveDestiny destiny)
        {
            return _dataStorage.ById<WizardLevelCollection>(destiny.IdDestiny)?.GetByLevel(destiny.Level);
        }


    }

    public class GenerateMobBlueprintCounter
    {
        public MobBlueprint MobBlueprint;
        public int TotalAmount;
    }
}