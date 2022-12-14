using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentBehaviourTree
{
    /// <summary>
    /// Fluent API for building a behaviour tree.
    /// </summary>
    public class BehaviourTreeBuilder
    {
        /// <summary>
        /// Last node created.
        /// </summary>
        private IBehaviourTreeNode curNode = null;

        /// <summary>
        /// Stack node nodes that we are build via the fluent API.
        /// </summary>
        private Stack<IParentBehaviourTreeNode> parentNodeStack = new Stack<IParentBehaviourTreeNode>();

        /// <summary>
        /// Create an action node.
        /// </summary>
        public BehaviourTreeBuilder Do(string name, Func<TimeData, BehaviourTreeStatus> fn)
        {
            if (parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }

            var actionNode = new ActionNode(name, fn);
            parentNodeStack.Peek().AddChild(actionNode);
            return this;
        }
        
        /// <summary>
        /// Create an action node.
        /// </summary>
        public BehaviourTreeBuilder Do(IBehaviourTreeNode behaviourTreeNode)
        {
            if (parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }
            parentNodeStack.Peek().AddChild(behaviourTreeNode);
            return this;
        }

        /// <summary>
        /// Like an action node... but the function can return true/false and is mapped to success/failure.
        /// </summary>
        public BehaviourTreeBuilder Condition(string name, Func<TimeData, bool> fn)
        {
            return Do(name, t => fn(t) ? BehaviourTreeStatus.Success : BehaviourTreeStatus.Failure);
        }

        /// <summary>
        /// Create an inverter node that inverts the success/failure of its children.
        /// </summary>
        public BehaviourTreeBuilder Inverter(string name)
        {
            var inverterNode = new InverterNode(name);

            return Push(inverterNode);
        }

        /// <summary>
        /// Create a sequence node.
        /// </summary>
        public BehaviourTreeBuilder Sequence(string name)
        {
            var sequenceNode = new SequenceNode(name);

            return Push(sequenceNode);
        }
        
        /// <summary>
        /// Create a sequence node.
        /// </summary>
        public BehaviourTreeBuilder Sequence(ISequenceNode sequenceNode)
        {
            return Push(sequenceNode);
        }
        
        /// <summary>
        /// Create a sequence node.
        /// </summary>
        public BehaviourTreeBuilder Parent(IParentBehaviourTreeNode sequenceNode)
        {
            return Push(sequenceNode);
        }

        /// <summary>
        /// Create a parallel node.
        /// </summary>
        public BehaviourTreeBuilder Parallel(string name, int numRequiredToFail, int numRequiredToSucceed)
        {
            var parallelNode = new ParallelNode(name, numRequiredToFail, numRequiredToSucceed);

            return Push(parallelNode);
        }

        /// <summary>
        /// Create a selector node.
        /// </summary>
        public BehaviourTreeBuilder Selector<T>(string name) where T :class, ISelectorNode
        {
            var selectorNode = Activator.CreateInstance(typeof(T), name) as T;
            return Push(selectorNode);
        }
        
        public BehaviourTreeBuilder Push(IParentBehaviourTreeNode node)
        {
            if (parentNodeStack.Count > 0)
            {
                parentNodeStack.Peek().AddChild(node);
            }

            parentNodeStack.Push(node);
            return this;
        }
        
        

        /// <summary>
        /// Splice a sub tree into the parent tree.
        /// </summary>
        public BehaviourTreeBuilder Splice(IBehaviourTreeNode subTree)
        {
            if (subTree == null)
            {
                throw new ArgumentNullException("subTree");
            }

            if (parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't splice an unnested sub-tree, there must be a parent-tree.");
            }

            parentNodeStack.Peek().AddChild(subTree);
            return this;
        }

        /// <summary>
        /// Build the actual tree.
        /// </summary>
        public IBehaviourTreeNode Build()
        {
            if (curNode == null)
            {
                throw new ApplicationException("Can't create a behaviour tree with zero nodes");
            }
            return curNode;
        }

        /// <summary>
        /// Ends a sequence of children.
        /// </summary>
        public BehaviourTreeBuilder End()
        {
            curNode = parentNodeStack.Pop();
            return this;
        }

        public BehaviourTreeBuilder Selector<T>(string name, IBehaviourTreeNode[] toArray) where T : class, ISelectorNode
        {
            var selector = Selector<T>(name);
            for (int i = 0; i < toArray.Length; i++)
            {
                selector.Do(toArray[i]);
            }
            return selector;

        }

      
    }
}
