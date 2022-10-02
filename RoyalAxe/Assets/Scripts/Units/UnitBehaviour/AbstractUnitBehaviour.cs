using FluentBehaviourTree;

namespace RoyalAxe.Units.UnitBehaviour
{
    public abstract class AbstractUnitBehaviour : AbstractUnitEntityView, IUnitBehaviourNode
    {
        public virtual string NodeName => GetType().Name;


        protected override void OnInit()
        {
            Unit.ReplaceUnitBehavior(this);
        }

        public virtual BehaviourTreeStatus Execute(TimeData time)
        {
            return BehaviourTreeStatus.Running;
        }

        private void OnBecameInvisible()
        {
            OnDeathEnd();
        }

        public void OnDeathEnd()
        {
            if (Unit.isEnabled)
            {
                Unit.isDestroyUnit = true;
            }
        }
    }
}