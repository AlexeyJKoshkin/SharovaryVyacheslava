using Core;
using UnityEngine;

namespace RoyalAxe.CoreLevel {
    /// <summary>
    /// Штука которая умеет заспавнить мобов по текущим параметрам
    /// </summary>
    public class MobSpawnOperation : IMobSpawnOperation
    {
        private readonly IRoyalAxeCoreMap _map;
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
            var deltaMob = _levelWaveProvider.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            if (deltaMob <= 0) return;
          
            _coreGamePlay.levelWaveEntity.isWaveFinished = true; // т.к. надо генерить еще мобов, а мобы закончились. значит волна закончена
        }


        private void SpawnWhileCan(IEnemyWaveGenerator mobGeneratorHelper)
        {
            /* при генерации всегда выполняются два условия
             1. мобы всегда генерируются ЗА экраном
             2. Мобов всегда генерируем пачкой. за 1 кадр . для этого мобов надо предсоздать в пуле. 
            */
            var needMob = _levelWaveProvider.MaxMobAmount - mobGeneratorHelper.CurrentMobAmount;
            int counter = 0;
            while (counter < needMob && _levelWaveProvider.HasMob) // создаем мобов пока можем
            {
                var mobData = _levelWaveProvider.GenerateMobData();
                mobGeneratorHelper.GenerateEnemy(mobData);
                counter++;
            }
            HLogger.LogCoreLevel($"Need {needMob} Created {counter}");
        }
    }
}