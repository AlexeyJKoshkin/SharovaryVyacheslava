using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using GameKit;
using RoyalAxe.GameEntitas;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel
{
    public class WaveLevelSwitcher : ILevelWaveLoader
    {
        public bool HasWave => _waveQueue.Count > 0;
        public int WaveNumber => _waveEntity.levelNumber.Number;
        public float SpawnCooldown => _currentSettings.SpawnCooldown;
        public int MaxMobAmount => _currentSettings.MaxMobAmount;
        public MobDeathReward CurrentMobReward => _currentSettings.MobDeathReward;
        public bool HasMob => _blueprints.Count > 0;
        public LevelBuffType[] CurrentBuffs => _waveEntity.hasWizardShopReady ? _waveEntity.wizardShopReady.LevelBuffTypes : new LevelBuffType[0];

        readonly Queue<LevelGeneratorSettings> _waveQueue = new Queue<LevelGeneratorSettings>();

        private readonly CoreGamePlayEntity _waveEntity;

        private LevelGeneratorSettings _currentSettings;

        private readonly IDataStorage _dataStorage;

        private readonly IMobBlueprintsSpawnStorage _mobBlueprintsSpawnStorage;
        readonly ICurrentUserProgressProfileFacade _userProgressProfileFacade;
        private readonly List<GenerateMobBlueprintCounter> _blueprints = new List<GenerateMobBlueprintCounter>();

        public WaveLevelSwitcher(CoreGamePlayContext coreGamePlayContext, IDataStorage dataStorage, IMobBlueprintsSpawnStorage mobBlueprintsSpawnStorage, ICurrentUserProgressProfileFacade userProgressProfileFacade)
        {
            _dataStorage            = dataStorage;
            _mobBlueprintsSpawnStorage = mobBlueprintsSpawnStorage;
            _userProgressProfileFacade = userProgressProfileFacade;
            _waveEntity             = coreGamePlayContext.CreateEntity();
            _waveEntity.isLevelWave = true;
        }

        public bool NextWave()
        {
            if (_waveQueue.Count == 0) return false;

            int nextWave         = WaveNumber + 1;
            var nextWaveSettings = _waveQueue.Dequeue();

            if (_currentSettings == null || _currentSettings.Type == nextWaveSettings.Type)
            {
                SetNewWave(nextWaveSettings);
                _waveEntity.ReplaceLevelNumber(nextWave);
                return true;
            }

            return false;
        }

        public void Init(ICoreLevelDataInfrastructure levelData)
        {
            _waveQueue.Clear();
            levelData.PackLevels.ForEach(e => _waveQueue.Enqueue(e));
            _mobBlueprintsSpawnStorage.PrepareBlueprints(levelData.PackLevels.SelectMany(o => o.MobsData));

            _waveEntity.AddLevelNumber(0);
        }

    


        private void SetNewWave(LevelGeneratorSettings nextWaveSettings)
        {
            _currentSettings = nextWaveSettings;
            _waveEntity.ReplaceLevelNumber(WaveNumber);
            _mobBlueprintsSpawnStorage.CreateWavePack(nextWaveSettings.MobsData);
            _blueprints.AddRange(_mobBlueprintsSpawnStorage.CreateWavePack(nextWaveSettings.MobsData)); // подготовили следующую пачку мобов
            _waveEntity.isWaveFinished = false;
            HandleDestiny(nextWaveSettings.Destiny);

            _userProgressProfileFacade.LevelProgressFacade.UpdateLastPlayedLevel(nextWaveSettings);
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

            if (_waveEntity.hasWizardShopReady)
                _waveEntity.RemoveWizardShopReady();
        }

        private WizardShopSettings GetWizardShop(WaveDestiny destiny)
        {
            return _dataStorage.ById<WizardLevelCollection>(destiny.IdDestiny)?.GetByLevel(destiny.Level);
        }

        public MobBlueprint GenerateMobData()
        {
            var item = _blueprints.GetRandom(false);
            item.TotalAmount--;
            if (item.TotalAmount == 0) _blueprints.Remove(item);
            return item.MobBlueprint;
        }
    }
    
   public class GenerateMobBlueprintCounter
    {
        public MobBlueprint MobBlueprint;
        public int TotalAmount;
    }
}