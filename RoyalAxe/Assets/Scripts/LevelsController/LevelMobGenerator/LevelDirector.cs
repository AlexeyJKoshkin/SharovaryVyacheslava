using Core;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel {
    public class LevelDirector : IDoneTimerListener,ILevelDirector
    {
        private readonly IRoyalAxeCoreMap _map;
     
        private readonly IRATimer _spawnCooldownTimer;
        private readonly ILevelWaveLoader _levelWaveLoader;
        private CoreGamePlayEntity _wave;
        
        bool HasMob => _wave.hasMobWaveCollection && _wave.mobWaveCollection.HasMobs;
        
        public LevelDirector(IRoyalAxeCoreMap map, ITimerFactory timerFactory, ILevelWaveLoader levelWaveLoader)
        {
            _map                = map;
            _levelWaveLoader  = levelWaveLoader;
            _spawnCooldownTimer = timerFactory.CreateTimer(0, true);
            _spawnCooldownTimer.AddDoneHandler(this);
        }


        public void StartLevel(ICoreLevelDataInfrastructure infrastructure)
        {
             _wave =  _levelWaveLoader.InitWaves(infrastructure.PackLevels);
             StartWaveImmediate();
        }
        
        public void StartWaveImmediate()
        {
            _spawnCooldownTimer.Run(_levelWaveLoader.SpawnCooldown);
            ExecuteTimerHandler();
        }

        void IDoneTimerListener.OnDoneTimer(GameRootLoopEntity entity)
        {
            ExecuteTimerHandler();
        }

        private void ExecuteTimerHandler()
        {
            var mobGeneratorHelper =  _map.StartGenerateMobPosition(); // получаем хелпер для генерации позиций мобу
            SpawnWhileCan(mobGeneratorHelper);
            var deltaMob =  _levelWaveLoader.MaxMobAmount- mobGeneratorHelper.CurrentMobAmount;
            if (deltaMob <= 0) return;
            LoadNextOrFinish(mobGeneratorHelper);
        }
        
        private void SpawnWhileCan(IEnemyWaveGenerator mobGeneratorHelper)
        {
            /* при генерации всегда выполняются два условия
             1. мобы всегда генерируются ЗА экраном
             2. Мобов всегда генерируем пачкой. за 1 кадр . для этого мобов надо предсоздать в пуле. 
            */
         
            var needMob = _levelWaveLoader.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            while (needMob>0 && HasMob) // создаем мобов пока можем
            {
                var modData = GenerateMobDataForSpawn();
                mobGeneratorHelper.GenerateEnemy(modData.mobId, modData.mobLevel);
                needMob--;
            }
        }
        
        (string mobId, byte mobLevel) GenerateMobDataForSpawn()
        {
            var mobData = _wave.mobWaveCollection.Generate();
            return (mobData.MobId, mobData.Level);
        }

        private void LoadNextOrFinish(IEnemyWaveGenerator mobGeneratorHelper)
        {
            if (_levelWaveLoader.NextWave()) // пробуем загрузить волну
            {
                if (_levelWaveLoader.HasWizard)
                {
                    SpawnWizard();
                    return;
                }
                _spawnCooldownTimer.Run(_levelWaveLoader.SpawnCooldown); //запускаем таймер с новым значением
            }
            else // неполучилось загрузить волну 
            {
                //ждем пока не останется мобов на поле
                if (mobGeneratorHelper.CurrentMobAmount == 0)
                {
                    DoEndLevelWin();
                }    
            }
        }

        private void SpawnWizard()
        {
            _spawnCooldownTimer.IsRunning = false; // тормозим таймер спавна мобов
            HLogger.LogError("Spawn Wizard");
        }

        private void DoEndLevelWin()
        {
          //  _spawnCooldownTimer.RemoveDoneHandler(this);
            HLogger.TempLog("Конец волн текущего биома");
        }
    }
}