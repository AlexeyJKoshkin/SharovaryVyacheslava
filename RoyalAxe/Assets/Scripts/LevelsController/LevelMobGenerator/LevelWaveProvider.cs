using System.Linq;
using Core.Data.Provider;

namespace RoyalAxe.CoreLevel
{
    public class LevelWaveProvider : ILevelWaveProvider
    {
        public int WaveNumber => _waveEntity.waveNumber.Number;
        public float SpawnCooldown => _currentSettings.SpawnCooldown;
        public int MaxMobAmount => _currentSettings.MaxMobAmount;
        public MobDeathReward CurrentMobReward => _currentSettings.MobDeathReward;
      

        private readonly CoreGamePlayEntity _waveEntity;

        private LevelGeneratorSettings _currentSettings;
        private readonly IDataStorage _dataStorage;

        public LevelWaveProvider(IDataStorage dataStorage, CoreGamePlayContext coreGamePlayContext)
        {
            _dataStorage = dataStorage;
            _waveEntity = coreGamePlayContext.CreateEntity();
            _waveEntity.AddWaveNumber(0);
        }
        
        public CoreGamePlayEntity LoadWave(int waveNumber)
        {
            _waveEntity.ReplaceWaveNumber(waveNumber-1);
            NextWave();
            return _waveEntity;
        }

        public bool NextWave()
        {
            int nextWave = WaveNumber + 1;
            var nextWaveSettings = _dataStorage.ById<LevelGeneratorSettings>(nextWave.ToString());
            if (nextWaveSettings !=null && (_currentSettings == null || _currentSettings.Type == nextWaveSettings.Type))
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
        }
    }
}
