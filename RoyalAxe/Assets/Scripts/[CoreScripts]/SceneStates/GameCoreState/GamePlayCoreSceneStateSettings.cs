using System;
using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CoreLevel;
using RoyalAxe.EntitasSystems;
using RoyalAxe.UI;

namespace Core.Launcher
{
    [Serializable]
    public class GamePlayCoreSceneStateSettings : ProjectStateSettings
    {
        private List<FeatureBindInfo> _pauseAble;
        private List<FeatureBindInfo> _alwaysUpdate;

        public override IEnumerable<Feature> EventListenerSystem(Contexts contexts)
        {
            yield return new GameRootLoopEventSystems(contexts);
            yield return new CoreGamePlayEventSystems(contexts);
            yield return new SkillEventSystems(contexts);
            yield return new UnitsEventSystems(contexts);
        }

        public override IEnumerable<FeatureBindInfo> AlwaysUpdate()
        {
            return _alwaysUpdate;
        }

        public override IEnumerable<FeatureBindInfo> PauseableUpdate()
        {
            return _pauseAble;
        }

        public override IEnumerable<Type> AllSystems()
        {
            foreach (var t in GetFrom(AlwaysUpdate())) yield return t;

            foreach (var t in GetFrom(PauseableUpdate())) yield return t;
        }

        public override void CreateFeatureBlanks()
        {
            _pauseAble = new List<FeatureBindInfo>
            {
                new UnitsFeatureBindInfo(),
                CleaningSystems(),
                GeneralCoreGameSystems()
            };

            _alwaysUpdate = new List<FeatureBindInfo>()
            {
                FeatureBindInfo.Create("UI")
                               .Bind<UICommandExecuteSystem>()
            };
        }


        private FeatureBindInfo GeneralCoreGameSystems()
        {
            return FeatureBindInfo.Create("Общие кор системы")
                                  .Bind<ChunkMovingSystem>()
                                  .Bind<LevelExperienceSystem>()
                                  .Bind<SaveTempProgressEachLevel>()
                                  .Bind<SaveUserProgressEvery10Level>();
        }

        private FeatureBindInfo CleaningSystems()
        {
            return FeatureBindInfo.Create("Завершающие системы")
                                  .Bind<DropSkillsTimerSystem>()          // Сбрасываем таймер на скилах
                                  .Bind<ClearTargetsSystem>()             // очистка списка целей  
                                  .Bind<ClearPhysicalInteractionSystem>() // очистка взаимодействией
                                  .Bind<HealthEventSystem>()              // обновение система слежения здоровья по юнитам
                                  .Bind<CheckUnitDeadSystem>()            // Выставляем что юнит помер
                                  .Bind<HandlePlayerDeadSystem>()         // проверяем помер ли игрок
                                  .Bind<DestroyUnitSystem>();             // уничтожение объектов

        }


        private IEnumerable<Type> GetFrom(IEnumerable<FeatureBindInfo> data)
        {
            return data.SelectMany(t => t.FeatureSystems);
        }
    }
}