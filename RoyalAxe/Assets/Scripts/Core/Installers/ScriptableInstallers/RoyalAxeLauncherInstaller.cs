using System;
using System.Collections.Generic;
using Core.Launcher;
using Entitas;
using RoyalAxe.Entitas.Systems.TimersSystem;
using RoyalAxe.EntitasSystems;
using RoyalAxe.EntitasSystems.TimerUtility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/RoyalAxeLauncher", fileName = "RoyalAxeLauncherInstaller")]
    public class RoyalAxeLauncherInstaller : ScriptableInstaller
    {
        private readonly List<Type> _updateSystems = new List<Type>
        {
            typeof(GameCoreSystems),         // обновляем состояние игры
            typeof(GameStateActivateSystem), // производим загрузку/выгрузку стейтов
            typeof(CalcTimerSystem),         // обновляем таймеры
            typeof(TimerEventSystem),        // вызов тиков таймеров
            typeof(DoneTimerEventSystem),    // обработка окончание таймеры
            typeof(DoneTimerSystem)          //окончание таймеры удаление/перезапуск
        };


        protected override void InstallBindings()
        {
            HLogger.Log("Core Binding");
            var shared = Contexts.sharedInstance;
            BindContexts(shared);
            Container.RegisterInstance(this).AsSelf();
            _updateSystems.ForEach(t => Container.Register(t, Lifetime.Singleton).AsSelf().AsImplementedInterfaces());
            Container.Register<RATimerFactory>(Lifetime.Singleton).AsImplementedInterfaces();

            Container.Register<SceneSystemBuilder<ISystem>>(Lifetime.Singleton)
                     .As<IRoyalAxeCoreSystemsBuilder>(); //биндим строителя систем для ядра игры

            InstallMainLoop();
        }

        private void BindContexts(Contexts shared)
        {
            Container.RegisterInstance(shared).AsSelf();
            foreach (object context in shared.allContexts) Container.RegisterInstance(context).AsSelf().AsImplementedInterfaces();
        }


        private void InstallMainLoop()
        {
            Container.RegisterEntryPoint<GameRootLauncher>();
        }
    }
}