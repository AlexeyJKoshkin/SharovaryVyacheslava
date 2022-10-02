﻿using System;
using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CoreLevel;
using RoyalAxe.EntitasSystems;

namespace Core.Launcher
{
    [Serializable]
    public class GamePlayCoreSceneStateSettings : ProjectStateSettings
    {
        private List<FeatureBindInfo> _updates;

        public override IEnumerable<Feature> EventListenerSystem(Contexts contexts)
        {
            //yield return new GameRootLoopEventSystems(contexts);
            yield return new CoreGamePlayEventSystems(contexts);
            yield return new SkillEventSystems(contexts);
            yield return new UnitsEventSystems(contexts);
        }

        public override IEnumerable<FeatureBindInfo> AlwaysUpdate()
        {
            yield break;
            
        }

        public override IEnumerable<FeatureBindInfo> PauseableUpdate()
        {
            return _updates;
        }

        public override IEnumerable<Type> AllSystems()
        {
            foreach (var t in GetFrom(AlwaysUpdate())) yield return t;

            foreach (var t in GetFrom(PauseableUpdate())) yield return t;
        }

        public override void CreateFeatureBlanks()
        {
            _updates = new List<FeatureBindInfo>
            {
               new UnitsFeatureBindInfo(),
                CleaningSystems(),
                ChunkSystems()
            };
        }

        private FeatureBindInfo ChunkSystems()
        {
            return FeatureBindInfo.Create("Общие кор системы")
                .Bind<ChunkMovingSystem>();
        }

        private FeatureBindInfo CleaningSystems()
        {
            return FeatureBindInfo.Create("Завершающие системы",
                                          typeof(DropSkillsTimerSystem),          // Сбрасываем таймер на скилах
                                    
                                          typeof(ClearTargetsSystem),             // очистка списка целей  
                                          typeof(ClearPhysicalInteractionSystem), // очистка взаимодействией
                                          typeof(ClearHitAndAttackSystem),        // очистка списка урона
                                          typeof(HealthEventSystem),              // обновение система слежения здоровья по юнитам
                                          typeof(HandlePlayerDeadSystem),         // проверяем помер ли игрок
                                          typeof(CheckMobDeadSystem),             // проверяем померли мобы
                                          typeof(DestroyUnitSystem));             // уничтожение объектов
        }


        private IEnumerable<Type> GetFrom(IEnumerable<FeatureBindInfo> data)
        {
            return data.SelectMany(t => t.FeatureSystems);
        }
    }
}