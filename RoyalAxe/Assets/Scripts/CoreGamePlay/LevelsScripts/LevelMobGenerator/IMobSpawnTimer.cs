using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel 
{
    public interface IMobSpawnFacade
    {
        void StopSpawn();
        void StartSpawnMob();
    }

    public class MobSpawnFacade : IMobSpawnFacade,IDoneTimerListener
    {
        private readonly CoreGamePlayContext _waveProvider;
        private readonly IMobSpawnOperation _mobSpawnOperation;
        private readonly IRATimer _spawnCooldownTimer;
        
        public MobSpawnFacade(ITimerFactory timerFactory, 
                              CoreGamePlayContext waveProvider, IMobSpawnOperation mobSpawnOperation)
        {
            _waveProvider = waveProvider;
            _mobSpawnOperation = mobSpawnOperation;
            _spawnCooldownTimer = timerFactory.CreateTimer(0, true);
            _spawnCooldownTimer.AddDoneHandler(this);
        }

        void IDoneTimerListener.OnDoneTimer(GameRootLoopEntity entity)
        {
            _mobSpawnOperation.SpawnMobs();
        }
        
        void StopMobTimer()
        {
            _spawnCooldownTimer.IsRunning = false;
        }

        public void ContinueMobTimer()
        {
            _spawnCooldownTimer.IsRunning = true;
        }


        public void StopSpawn()
        {
            StopMobTimer();
            
        }

        public void StartSpawnMob()
        {
            _spawnCooldownTimer.Run(_waveProvider.levelWaveEntity.levelWaveQueue.Current.SpawnCooldown);; // запускаем таймер
            _mobSpawnOperation.SpawnMobs();  // первую волну спавним на старте
        }

        public void PauseSpawn()
        {
            StopMobTimer();
        }
    }
}