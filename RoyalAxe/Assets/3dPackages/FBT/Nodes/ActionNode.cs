using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentBehaviourTree
{
    public abstract class AbstractBTNode : IBehaviourTreeNode
    {
        public abstract string NodeName { get; }

        public abstract BehaviourTreeStatus Execute(TimeData time);

    }
    
    /// <summary>
    /// A behaviour tree leaf node for running an action.
    /// </summary>
    public class ActionNode :AbstractBTNode, IBehaviourTreeNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        public override string NodeName { get; }

        /// <summary>
        /// Function to invoke for the action.
        /// </summary>
        private Func<TimeData, BehaviourTreeStatus> fn;
        

        public ActionNode(string name, Func<TimeData, BehaviourTreeStatus> fn)
        {
            this.NodeName=name;
            this.fn=fn;
        }

        public sealed override BehaviourTreeStatus Execute(TimeData time)
        {
            return fn(time);
        }
    }
}
