using Core;
using UnityEngine;

namespace RoyalAxe.CoreLevel {
    /// <summary>
    /// Штука которая умеет заспавнить мобов по текущим параметрам
    /// </summary>
    public class MobSpawnOperation : IMobSpawnOperation
    {
        private readonly IRoyalAxeCoreMap _map;

        private CoreGamePlayEntity Wave => _coreGamePlay.levelWaveEntity;
        private readonly CoreGamePlayContext _coreGamePlay;

        private readonly ILevelWaveLoader _levelWaveProvider;


        public MobSpawnOperation(IRoyalAxeCoreMap map,
                                 ILevelWaveLoader levelWaveLoader,
                                 CoreGamePlayContext coreGamePlay)
        {
            _map               = map;
            _levelWaveProvider = levelWaveLoader;
            _coreGamePlay      = coreGamePlay;
        }


        public void SpawnMobs()
        {
            var mobGeneratorHelper = _map.StartGenerateMobPosition(); // получаем хелпер для генерации позиций мобу
            SpawnWhileCan(mobGeneratorHelper);
            var deltaMob = _coreGamePlay.levelWaveEntity.levelWaveQueue.Current.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            if (deltaMob <= 0) return;
          
            _coreGamePlay.levelWaveEntity.isWaveFinished = true; // т.к. надо генерить еще мобов, а мобы закончились. значит волна закончена
        }


        private void SpawnWhileCan(IEnemyWaveGenerator mobGeneratorHelper)
        {
            /* при генерации всегда выполняются два условия
             1. мобы всегда генерируются ЗА экраном
             2. Мобов всегда генерируем пачкой. за 1 кадр . для этого мобов надо предсоздать в пуле. 
            */
            var needMob =Wave.levelWaveQueue.Current.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;

            var blueprints = Wave.levelMobBluePrints;
            
            int counter = 0;
            while (counter < needMob && blueprints.Count > 0) // создаем мобов пока можем
            {
                var mobData = blueprints.GenerateMobData();
                mobGeneratorHelper.GenerateEnemy(mobData);
                counter++;
            }
            HLogger.LogCoreLevel($"Need {needMob} Created {counter}");
        }
    }
}