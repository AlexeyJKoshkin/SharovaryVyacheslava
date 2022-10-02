using Core;
using Core.Launcher;
using Entitas;
using FluentBehaviourTree;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    /*
     * Переключение между стейтами. Каждый тик дергает текущией стейт игры для обновления своего состояния.
     */
    public class GameCoreSystems : IExecuteSystem, IInitializeSystem, IMainGameContext
    {
        private readonly FMSStateCollection<IProjectSceneState> _fmsStateCollection;

        /// <summary>
        ///     основное состояние игры
        /// </summary>
        private IBehaviourTreeNode _mainLoopBT;

        public GameCoreSystems(ISceneLoaderSceneState sceneLoaderSceneState, GameRootLoopContext context)
        {
            _fmsStateCollection = new FMSStateCollection<IProjectSceneState>(new[]
            {
                sceneLoaderSceneState,
                (IProjectSceneState) new MockState()
            });
        }

        public void Execute()
        {
            var result = _mainLoopBT.Execute(TimeData.Last);

            if (result == BehaviourTreeStatus.Failure)
            {
                Debug.LogError("Что-то пошло не так в текущем стейты игры. Надо что-то где то выводить и чепятать в лог.");
            }
        }

        public void Initialize()
        {
            HLogger.LogInfo("Init GameCore Systems");

            _mainLoopBT = new BehaviourTreeBuilder()
                          .Sequence("Тик стейта сцены")                                  // Постоянно крутим в цикле два действия
                          .Do("Обновить текущий стейт", UpdateCurrentState)              // стейт сцены подменяется на текущий при запуске сцены
                          .Do("Установили текущий стейт загрузка сцены", HandleEndState) //
                          .End().Build();
            _fmsStateCollection.SetState<MockState>();
        }

        private BehaviourTreeStatus HandleEndState(TimeData arg)
        {
            return BehaviourTreeStatus.Success;
        }

        private BehaviourTreeStatus UpdateCurrentState(TimeData dt)
        {
            var result = _fmsStateCollection.Current.Execute(dt);
            //        HLogger.LogInfo($"Update {_fmsStateCollection.Current.NodeName} {result}");
            //т.к текущий стейт всегда выставляется ручками, он существует всегда -обновляем текущий стейт. 
            return result;
        }

        /// <summary>
        ///     новую загруженную сцену выставляем в текущий стейт.
        /// </summary>
        void IMainGameContext.HandleNewState(IProjectSceneState newSceneState)
        {
            _fmsStateCollection.SetCurrent(newSceneState);
        }
    }
}