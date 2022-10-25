using System.Collections.Generic;


namespace FluentBehaviourTree
{
    public interface ISequenceNode : IParentBehaviourTreeNode
    {
        
    }

    /// <summary>
    /// Runs child nodes in sequence, until one fails.
    /// </summary>
    public class SequenceNode :AbstractBTNode, ISequenceNode
    {
        /// <summary>
        /// Name of the node.
        /// </summary>
        public override string NodeName { get; }

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>(); //todo: this could be optimized as a baked array.

        public SequenceNode(string name)
        {
            this.NodeName = name;
        }

        public sealed override BehaviourTreeStatus Execute(TimeData time)
        {
            foreach (var child in children)
            {
                var childStatus = child.Execute(time);
                if (childStatus != BehaviourTreeStatus.Success)
                {
                    return childStatus;
                }
            }

            return BehaviourTreeStatus.Success;
        }

        /// <summary>
        /// Add a child to the sequence.
        /// </summary>
        public void AddChild(IBehaviourTreeNode child)
        {
            children.Add(child);
        }
    }
}
