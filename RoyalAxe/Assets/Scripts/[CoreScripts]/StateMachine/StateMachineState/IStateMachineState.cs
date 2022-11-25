using FluentBehaviourTree;

namespace Core
{
    public interface IFMSState : IBehaviourTreeNode
    {
        void ExitState();
        void EnterState();
    }
}