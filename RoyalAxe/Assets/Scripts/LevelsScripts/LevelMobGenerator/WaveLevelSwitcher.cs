﻿using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.CoreLevel
{
    public class WaveLevelSwitcher : ILevelWaveLoader
    {
        public int WaveNumber => _waveEntity.levelNumber.Number;
        public float SpawnCooldown => _currentSettings.SpawnCooldown;
        public int MaxMobAmount => _currentSettings.MaxMobAmount;
        public MobDeathReward CurrentMobReward => _currentSettings.MobDeathReward;
        public bool HasMob => _waveEntity.mobWaveCollection.HasMobs;
        public LevelBuffType[] CurrentBuffs => _waveEntity.hasWizardShopReady ? _waveEntity.wizardShopReady.LevelBuffTypes : new LevelBuffType[0];

        readonly Queue<LevelGeneratorSettings> _waveQueue = new Queue<LevelGeneratorSettings>();

        private readonly CoreGamePlayEntity _waveEntity;

        private LevelGeneratorSettings _currentSettings;

        private readonly IDataStorage _dataStorage;

        public WaveLevelSwitcher(CoreGamePlayContext coreGamePlayContext, IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            _waveEntity = coreGamePlayContext.CreateEntity();
            _waveEntity.isLevelWave = true;
        }

        public bool NextWave()
        {
            if (_waveQueue.Count == 0) return false;

            int nextWave = WaveNumber + 1;
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
            _waveEntity.AddLevelNumber(0);
        }

        private void SetNewWave(LevelGeneratorSettings nextWaveSettings)
        {
            _currentSettings = nextWaveSettings;
            _waveEntity.ReplaceLevelNumber(WaveNumber); // номер уровня/волны
            _waveEntity.ReplaceMobWaveCollection(nextWaveSettings.MobsData.Select(o => new MobAtLevelData(o)).ToList()); //обновляем список мобов для спавна
            _waveEntity.isWaveFinished = false;
            HandleDestiny(nextWaveSettings.Destiny);
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
            
            if(_waveEntity.hasWizardShopReady)
                _waveEntity.RemoveWizardShopReady();

        }

        private WizardShopSettings GetWizardShop(WaveDestiny destiny)
        {
            return _dataStorage.ById<WizardLevelCollection>(destiny.IdDestiny)?.GetByLevel(destiny.Level);
        }

        public MobAtLevelData GenerateMobData()
        {
            return _waveEntity.mobWaveCollection.Generate();
        }

        public bool HasWave => _waveQueue.Count > 0;
    }
}