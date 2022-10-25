using System.Collections.Generic;
using System.Linq;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class WaveLevelSwitcher : ILevelWaveLoader
    {
        public int WaveNumber => _waveEntity.waveNumber.Number;
        public float SpawnCooldown => _currentSettings.SpawnCooldown;
        public int MaxMobAmount => _currentSettings.MaxMobAmount;
        public MobDeathReward CurrentMobReward => _currentSettings.MobDeathReward;
        public bool HasMob => _waveEntity.mobWaveCollection.HasMobs;

        readonly Queue<LevelGeneratorSettings> _waveQueue = new Queue<LevelGeneratorSettings>();

        private readonly CoreGamePlayEntity _waveEntity;

        private LevelGeneratorSettings _currentSettings;

        public WaveLevelSwitcher(CoreGamePlayContext coreGamePlayContext, ICoreLevelDataInfrastructure dataInfrastructure)
        {
            _waveEntity = coreGamePlayContext.CreateEntity();
            _waveEntity.isLevelWave = true;
            InitWaves(dataInfrastructure.PackLevels);
        }

        void InitWaves(IReadOnlyList<LevelGeneratorSettings> infrastructurePackLevels)
        {
            infrastructurePackLevels.ForEach(e => _waveQueue.Enqueue(e));
            _waveEntity.ReplaceWaveNumber(0);
            NextWave();
        }

        public bool NextWave()
        {
            if (_waveQueue.Count == 0) return false;

            int nextWave = WaveNumber + 1;
            var nextWaveSettings = _waveQueue.Dequeue();

            if (_currentSettings == null || _currentSettings.Type == nextWaveSettings.Type)
            {
                SetNewWave(nextWaveSettings);
                _waveEntity.ReplaceWaveNumber(nextWave);
                return true;
            }

            return false;
        }

        private void SetNewWave(LevelGeneratorSettings nextWaveSettings)
        {
            _currentSettings = nextWaveSettings;
            _waveEntity.ReplaceWaveNumber(WaveNumber);
            _waveEntity.ReplaceMobWaveCollection(nextWaveSettings.MobsData.Select(o => new MobAtLevelData(o)).ToList());
            _waveEntity.isWaveFinished = false;
            _waveEntity.isWizardShopReady = Random.Range(0, 1) < 0.5f;
            _waveEntity.isWaveMobReady = !_waveEntity.isWizardShopReady; // стартуем мобов, если нет магазина
        }

        public MobAtLevelData GenerateMobData()
        {
            return _waveEntity.mobWaveCollection.Generate();
        }
    }
}
