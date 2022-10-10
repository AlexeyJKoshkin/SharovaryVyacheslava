using Core;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.CoreLevel {
    public class LevelDirector : IDoneTimerListener,ILevelDirector
    {
        private readonly IRoyalAxeCoreMap _map;
     
        private readonly IRATimer _spawnCooldownTimer;
        private readonly ILevelWaveProvider _levelWaveProvider;
        private CoreGamePlayEntity _wave;
        
        bool HasMob => _wave.hasMobWaveCollection && _wave.mobWaveCollection.HasMobs;
        
        public LevelDirector(IRoyalAxeCoreMap map, ITimerFactory timerFactory, ILevelWaveProvider levelWaveProvider)
        {
            _map                = map;
            _levelWaveProvider  = levelWaveProvider;
            _spawnCooldownTimer = timerFactory.CreateTimer(0, true);
            _spawnCooldownTimer.AddDoneHandler(this);
        }


        public void StartLevel()
        {
             _wave =  _levelWaveProvider.LoadWave(1); //todo: надо ли откуда-то грузить номер волны ?
            _spawnCooldownTimer.Run(_levelWaveProvider.SpawnCooldown);
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
            var deltaMob =  _levelWaveProvider.MaxMobAmount- mobGeneratorHelper.CurrentMobAmount;
            if (deltaMob <= 0) return;
            LoadNextOrFinish(mobGeneratorHelper);
          
        }

       

        private void SpawnWhileCan(IEnemyWaveGenerator mobGeneratorHelper)
        {
            var mobReward = _levelWaveProvider.CurrentMobReward;
            /* при генерации всегда выполняются два условия
             1. мобы всегда генерируются ЗА экраном
             2. Мобов всегда генерируем пачкой. за 1 кадр . для этого мобов надо предсоздать в пуле. 
            */
         
            var needMob = _levelWaveProvider.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            while (needMob>0 && HasMob) // создаем мобов пока можем
            {
                var modData = GenerateMobDataForSpawn();
                mobGeneratorHelper.GenerateEnemy(modData.mobId, modData.mobLevel, mobReward);
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
            if(_levelWaveProvider.NextWave()) // пробуем загрузить волну
                _spawnCooldownTimer.Run(_levelWaveProvider.SpawnCooldown); //запускаем таймер с новым значением
            else // неполучилось. 
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
            StartLevel();
        }
    }
}