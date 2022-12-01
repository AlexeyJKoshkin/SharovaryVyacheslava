using RoyalAxe.EntitasSystems.TimerUtility;
using VContainer.Unity;

namespace RoyalAxe.CoreLevel {
    public class MobSpawnFacade : IMobSpawnFacade,IDoneTimerListener, IGamePauseListener, IInitializable
    {
        private readonly CoreGamePlayContext _waveProvider;
        private readonly GameRootLoopContext _context;
        private readonly IMobSpawnOperation _mobSpawnOperation;
        private readonly IRATimer _spawnCooldownTimer;
        
        public MobSpawnFacade(ITimerFactory timerFactory, 
                              CoreGamePlayContext waveProvider, IMobSpawnOperation mobSpawnOperation, GameRootLoopContext context)
        {
            _waveProvider       = waveProvider;
            _mobSpawnOperation  = mobSpawnOperation;
            _context = context;
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
            _mobSpawnOperation.SpawnMobs();                                                               // первую волну спавним на старте
        }

        public void OnGamePause(GameRootLoopEntity entity, bool isPause)
        {
            if (isPause)
            {
                StopMobTimer();
            }
            else ContinueMobTimer();
        }

        public void Initialize()
        {
            _context.gamePauseEntity.AddGamePauseListener(this);
        }
    }
}