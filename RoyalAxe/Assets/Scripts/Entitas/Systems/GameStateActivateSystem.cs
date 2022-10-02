using System.Collections.Generic;
using Core.Data.Provider;
using Core.Installers;
using Core.Launcher;
using Entitas;
using GameKit;

namespace RoyalAxe.EntitasSystems
{
    public class GameStateActivateSystem : ReactiveSystem<GameRootLoopEntity>
    {
        private readonly LocalDataBoxStorage _localDataBoxStorage;
        private readonly GameRootUnityCallbackReceiver _unityCallbackReceiver;
        private readonly IMainGameContext _mainGameContext;
        private readonly ISceneLoaderSceneState _sceneLoaderSceneState;

        public GameStateActivateSystem(IContext<GameRootLoopEntity> context, GameRootUnityCallbackReceiver unityCallbackReceiver, LocalDataBoxStorage localDataBoxStorage, IMainGameContext mainGameContext, ISceneLoaderSceneState sceneLoaderSceneState) : base(context)
        {
            _unityCallbackReceiver = unityCallbackReceiver;
            _localDataBoxStorage   = localDataBoxStorage;
            _mainGameContext       = mainGameContext;
            _sceneLoaderSceneState = sceneLoaderSceneState;
        }

        private IMatcher<GameRootLoopEntity> _matcher;

        protected override ICollector<GameRootLoopEntity> GetTrigger(IContext<GameRootLoopEntity> context)
        {
            _matcher = Matcher<GameRootLoopEntity>.AllOf(GameRootLoopMatcher.MainLoopState,
                                                         GameRootLoopMatcher.AdditionalDataBox,
                                                         GameRootLoopMatcher.UpdateSystems,
                                                         GameRootLoopMatcher.PauseableUpdateSystems);
            return context.CreateCollector(_matcher.AddedOrRemoved());
        }

        protected override bool Filter(GameRootLoopEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameRootLoopEntity> entities)
        {
            var e = entities.SingleEntity();
            if (e.hasMainLoopState)
            {
                EnterState(e);
            }
            else
            {
                ExitState(e);
                e.Destroy();
            }
        }

        private void ExitState(GameRootLoopEntity state)
        {
            state.additionalDataBox.ForEach(_localDataBoxStorage.Remove);

            state.updateSystems.Collection.ForEach(_unityCallbackReceiver.RemoveUpdate);
            state.pauseableUpdateSystems.Collection.ForEach(_unityCallbackReceiver.RemoveUpdate);

            _mainGameContext.HandleNewState(_sceneLoaderSceneState);
        }

        private void EnterState(GameRootLoopEntity state)
        {
            
            state.additionalDataBox.ForEach(_localDataBoxStorage.Add);

            state.updateSystems.Collection.ForEach(_unityCallbackReceiver.AddUpdate);
            state.pauseableUpdateSystems.Collection.ForEach(_unityCallbackReceiver.AddPauseAbleUpdate);

            _mainGameContext.HandleNewState(state.mainLoopState.State);
        }
    }
}