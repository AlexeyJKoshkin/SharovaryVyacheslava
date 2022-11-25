using Entitas;
using FluentBehaviourTree;
using GameKit;
using RoyalAxe.CoreLevel;
using UnityEngine;

namespace Core.Launcher
{
    public class CoreGameState : RoyalAxeSceneState<ICoreGameInfrastructure>
    {
        private ILevelCreation LevelCreation => Infrastructure.LevelCreation;

        private IStateLoaderProvider StateLoaderProvider => Infrastructure.StateLoaderProvider;
        IBehaviourTreeNode _behaviour;

        public CoreGameState(ICoreGameInfrastructure coreGameInfrastructure) : base(coreGameInfrastructure)
        {
        }

        protected override void OnExecute(TimeData dt)
        {
            _behaviour.Execute(dt);
            var sceneLoader = StateLoaderProvider.GetCurrentSceneLoader();

            if (sceneLoader == null)
                Continue();
            else
            {
                LoadScene(sceneLoader);
            }
        }

        protected override void OnEnterState()
        {
            _behaviour = LevelCreation.CreateLevel();
            //по хорошему надо ждать анимации, всякое такое
            //стейт начал работать

            //  _axeSceneDependenciesConnector.EnterState(this);   
            LevelCreation.StartLevel();
        }

        protected override void OnExitState()
        {
            Infrastructure.LevelUtility.ClearAllBeforeLeaveCoreScene();
        }

      
    }
}
