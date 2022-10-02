/*
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluentBehaviourTree
{
    /// <summary>
    /// Fluent API for building a behaviour tree.
    /// </summary>
    public static class StaticBehaviourTreeBuilder
    {
        /// <summary>
        /// Last node created.
        /// </summary>
        private static IBehaviourTreeNode curNode = null;

        /// <summary>
        /// Stack node nodes that we are build via the fluent API.
        /// </summary>
        private static Stack<IParentBehaviourTreeNode> parentNodeStack = new Stack<IParentBehaviourTreeNode>();

        /// <summary>
        /// Create an action node.
        /// </summary>
        public static IParentBehaviourTreeNode Do(this IParentBehaviourTreeNode parent, string name, Func<TimeData, BehaviourTreeStatus> fn)
        {
            if (parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }

            var actionNode = new ActionNode(name, fn);
            parentNodeStack.Peek().AddChild(actionNode);
            return parent;
        }

        /// <summary>
        /// Create an action node.
        /// </summary>
        public static IParentBehaviourTreeNode Do(this IParentBehaviourTreeNode parent,IBehaviourTreeNode behaviourTreeNode)
        {
            if (parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }

            parentNodeStack.Peek().AddChild(behaviourTreeNode);
            return parent;
        }

        /// <summary>
        /// Like an action node... but the function can return true/false and is mapped to success/failure.
        /// </summary>
        public static IParentBehaviourTreeNode Condition(this IParentBehaviourTreeNode parent, string name, Func<TimeData, bool> fn)
        {
            return Do(parent,name, t => fn(t) ? BehaviourTreeStatus.Success : BehaviourTreeStatus.Failure);
        }

        /// <summary>
        /// Create an inverter node that inverts the success/failure of its children.
        /// </summary>
        public static IParentBehaviourTreeNode Inverter(this IParentBehaviourTreeNode parent,string name)
        {
            var inverterNode = new InverterNode(name);

            return Push(inverterNode);
        }

        /// <summary>
        /// Create a sequence node.
        /// </summary>
        public static IParentBehaviourTreeNode Sequence(this IParentBehaviourTreeNode parent,string name)
        {
            var sequenceNode = new SequenceNode(name);

            return Push(sequenceNode);
        }

        /// <summary>
        /// Create a parallel node.
        /// </summary>
        public static IParentBehaviourTreeNode Parallel(this IParentBehaviourTreeNode parent,string name, int numRequiredToFail, int numRequiredToSucceed)
        {
            var parallelNode = new ParallelNode(name, numRequiredToFail, numRequiredToSucceed);

            return Push(parallelNode);
        }

        /// <summary>
        /// Create a selector node.
        /// </summary>
        public static IParentBehaviourTreeNode Selector<T>(this IParentBehaviourTreeNode parent,string name) where T : class, ISelectorNode
        {
            var selectorNode = Activator.CreateInstance(typeof(T), name) as T;
            return Push(selectorNode);
        }
        

        public static IParentBehaviourTreeNode Push(IParentBehaviourTreeNode node)
        {
            if (parentNodeStack.Count > 0)
            {
                parentNodeStack.Peek().AddChild(node);
            }

            parentNodeStack.Push(node);
            return node;
        }

        /// <summary>
        /// Build the actual tree.
        /// </summary>
        public static IBehaviourTreeNode Build(this IBehaviourTreeNode node)
        {
            if (curNode != node)
            {
                throw new ApplicationException("Smthg go wrong!");
            }

            if (curNode == null)
            {
                throw new ApplicationException("Can't create a behaviour tree with zero nodes");
            }
            var result = curNode;
            curNode = null;
            parentNodeStack.Clear();
            return result;
        }

        /// <summary>
        /// Ends a sequence of children.
        /// </summary>
        public static IParentBehaviourTreeNode End(this IParentBehaviourTreeNode parent)
        {
            curNode = parentNodeStack.Pop();
            return parent;
        }

        public static IParentBehaviourTreeNode Selector<T>(this IParentBehaviourTreeNode parent, string name, IBehaviourTreeNode[] toArray) where T : class, ISelectorNode
        {
            var selector = Selector<T>(parent,name);

            for (int i = 0; i < toArray.Length; i++)
            {
                selector.Do(toArray[i]);
            }

            return parent;
        }
    }
}
*/
