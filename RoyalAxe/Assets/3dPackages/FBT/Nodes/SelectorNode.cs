using System.Collections.Generic;


namespace FluentBehaviourTree
{
    public interface IMemoryNode : IBehaviourTreeNode
    {
        IBehaviourTreeNode Last { get; }
    }

    public interface ISelectorNode :IParentBehaviourTreeNode
    {
        
    }

    /// <summary>
    /// Selects the first node that succeeds. Tries successive nodes until it finds one that doesn't fail.
    /// </summary>
    public class SelectorNode : AbstractBTNode, ISelectorNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        public override string NodeName { get; }

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>(); //todo: optimization, bake this to an array.

        public SelectorNode(string name)
        {
            this.NodeName = name;
        }

        public sealed override BehaviourTreeStatus Execute(TimeData time)
        {
            foreach (var child in children)
            {
                var childStatus = child.Execute(time);
                if (childStatus != BehaviourTreeStatus.Failure)
                {
                    return childStatus;
                }
            }

            return BehaviourTreeStatus.Failure;
        }

        /// <summary>
        /// Add a child node to the selector.
        /// </summary>
        public void AddChild(IBehaviourTreeNode child)
        {
            children.Add(child);
        }
    }

    public class SelectorWithMemory : AbstractBTNode,ISelectorNode,IMemoryNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        public override string NodeName { get; }

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>(); //todo: optimization, bake this to an array.

        public IBehaviourTreeNode Last { get; private set; }

        public SelectorWithMemory(string name)
        {
            this.NodeName = name;
        }

        public sealed override BehaviourTreeStatus Execute(TimeData time)
        {
            Last = null;
            foreach (var child in children)
            {
                var childStatus = child.Execute(time);
                if (childStatus != BehaviourTreeStatus.Failure)
                {
                    Last = child;
                    return childStatus;
                }
            }
            return BehaviourTreeStatus.Failure;
        }

        /// <summary>
        /// Add a child node to the selector.
        /// </summary>
        public void AddChild(IBehaviourTreeNode child)
        {
            children.Add(child);
        }

        public void Clear()
        {
            children.Clear();
        }
    }
}
