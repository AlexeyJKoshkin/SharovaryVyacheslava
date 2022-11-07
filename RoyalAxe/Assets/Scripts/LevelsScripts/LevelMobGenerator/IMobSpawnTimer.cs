using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel 
{
    public interface IMobSpawnTimer
    {
        void StopMobTimer();
        void ContinueMobTimer();
        void StartMobTimer();
    }

    public class MobSpawnTimer : IMobSpawnTimer, IDoneTimerListener 
    {
        private readonly ILevelWaveProvider _waveProvider;
        private readonly IMobSpawnOperation _mobSpawnOperation;
        private readonly IRATimer _spawnCooldownTimer;
        
        public MobSpawnTimer(ITimerFactory timerFactory, ILevelWaveProvider waveProvider, IMobSpawnOperation mobSpawnOperation)
        {
            _waveProvider = waveProvider;
            _mobSpawnOperation = mobSpawnOperation;
            _spawnCooldownTimer = timerFactory.CreateTimer(0, true);
            _spawnCooldownTimer.AddDoneHandler(this);
        }

        public void StopMobTimer()
        {
            _spawnCooldownTimer.IsRunning = false;
        }

        public void ContinueMobTimer()
        {
            _spawnCooldownTimer.IsRunning = true;
        }

        public void StartMobTimer()
        {
            _spawnCooldownTimer.Run(_waveProvider.SpawnCooldown);
        }

        public void OnDoneTimer(GameRootLoopEntity entity)
        {
            _mobSpawnOperation.SpawnMobs();
        }
    }
}