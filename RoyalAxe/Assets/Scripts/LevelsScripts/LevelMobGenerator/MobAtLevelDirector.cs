using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel
{
    public class MobAtLevelDirector : IMobAtLevelDirector
    {
        private readonly IRoyalAxeCoreMap _map;
        private readonly CoreGamePlayContext _coreGamePlay;
        
        private readonly ILevelWaveLoader _levelWaveProvider;

        private readonly IRATimer _spawnCooldownTimer;

        public MobAtLevelDirector(IRoyalAxeCoreMap map,
                                  ILevelWaveLoader levelWaveLoader,
                                  ITimerFactory timerFactory,
                                  CoreGamePlayContext coreGamePlay)
        {
            _map             = map;
            _levelWaveProvider = levelWaveLoader;
            _coreGamePlay = coreGamePlay;
            _spawnCooldownTimer = timerFactory.CreateTimer(0, true);
            _spawnCooldownTimer.AddDoneHandler(this);
        }

        
        public void StartWaveImmediate()
        {
            _spawnCooldownTimer.Run(_levelWaveProvider.SpawnCooldown);
            DoSpawnMob();
        }

        public void StopSpawn()
        {
            _spawnCooldownTimer.IsPause = true;
        }

        void IDoneTimerListener.OnDoneTimer(GameRootLoopEntity entity)
        {
            StartWaveImmediate();
        }

        void DoSpawnMob()
        {
            var mobGeneratorHelper = _map.StartGenerateMobPosition(); // получаем хелпер для генерации позиций мобу
            SpawnWhileCan(mobGeneratorHelper);
            var deltaMob = _levelWaveProvider.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            if (deltaMob <= 0) return;
            _coreGamePlay.levelWaveEntity.isWaveFinished = true; // т.к. надо генерить еще мобов, а мобы закончились. значит волна закончена
           // _wave.gems
            //_wave.
          //  LoadNextOrFinish(mobGeneratorHelper);
        }

        private void SpawnWhileCan(IEnemyWaveGenerator mobGeneratorHelper)
        {
            /* при генерации всегда выполняются два условия
             1. мобы всегда генерируются ЗА экраном
             2. Мобов всегда генерируем пачкой. за 1 кадр . для этого мобов надо предсоздать в пуле. 
            */

            var needMob = _levelWaveProvider.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            while (needMob > 0 && _levelWaveProvider.HasMob) // создаем мобов пока можем
            {
                var mobData = _levelWaveProvider.GenerateMobData();
                mobGeneratorHelper.GenerateEnemy(mobData.MobId, mobData.Level);
                needMob--;
            }
        }

        /*private void LoadNextOrFinish(IEnemyWaveGenerator mobGeneratorHelper)
        {
            if (_levelWaveLoader.NextWave()) // пробуем загрузить волну
            {
                return;
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


        private void DoEndLevelWin()
        {
            //  _spawnCooldownTimer.RemoveDoneHandler(this);
            HLogger.TempLog("Конец волн текущего биома");
        }*/
    }
}