using FluentBehaviourTree;
using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public class CoreGameState : RoyalAxeSceneState<ICoreGameInfrastructure>
    {
        private ILevelCreation LevelCreation => Infrastructure.LevelCreation;

        public CoreGameState(ICoreGameInfrastructure coreGameInfrastructure) : base(coreGameInfrastructure) { }

        protected override IBehaviourTreeNode GetBehavior()
        {
            
            var behaviour = LevelCreation.CreateLevel();
            LevelCreation.StartLevel();
            return behaviour;
        }

        protected override void OnExitState()
        {
            Infrastructure.LevelUtility.ClearAllBeforeLeaveCoreScene();
        }
    }
}