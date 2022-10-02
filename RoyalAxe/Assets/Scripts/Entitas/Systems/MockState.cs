using Core.Launcher;
using FluentBehaviourTree;

namespace RoyalAxe.EntitasSystems
{
    public class MockState : IProjectSceneState
    {
        public BehaviourTreeStatus Execute(TimeData time)
        {
            return BehaviourTreeStatus.Running;
        }

        public string NodeName => "Mock";
        public void ExitState() { }

        public void EnterState() { }
    }
}