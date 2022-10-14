using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class LevelWaveProvider : ILevelWaveLoader
    {
        public int WaveNumber => _waveEntity.waveNumber.Number;
        public float SpawnCooldown => _currentSettings.SpawnCooldown;
        public int MaxMobAmount => _currentSettings.MaxMobAmount;
        public MobDeathReward CurrentMobReward => _currentSettings.MobDeathReward;
        public bool HasWizard { get; private set; }
        
        readonly Queue<LevelGeneratorSettings> _waveQueue = new Queue<LevelGeneratorSettings>();

        private readonly CoreGamePlayEntity _waveEntity;

        private LevelGeneratorSettings _currentSettings;
        public LevelWaveProvider( CoreGamePlayContext coreGamePlayContext)
        {
            _waveEntity = coreGamePlayContext.CreateEntity();
            _waveEntity.AddWaveNumber(0);
        }
        
        public CoreGamePlayEntity InitWaves(IReadOnlyList<LevelGeneratorSettings> infrastructurePackLevels)
        {
            infrastructurePackLevels.ForEach(e=> _waveQueue.Enqueue(e));
            _waveEntity.ReplaceWaveNumber(0);
            NextWave();
            return _waveEntity;
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
           _waveEntity.ReplaceMobWaveCollection(nextWaveSettings.MobsData.Select(o=> new MobAtLevelData(o)).ToList());
          // HasWizard = Random.Range(0, 1) < 0.5f;
        }
    }
}
