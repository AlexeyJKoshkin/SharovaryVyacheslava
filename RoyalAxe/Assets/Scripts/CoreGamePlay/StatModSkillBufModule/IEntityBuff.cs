using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    /// <summary>
    ///     Какой-то баф, который висит на юните
    /// </summary>
    public interface IEntityBuff : IBehaviourTreeNode,IUnitApplierItem
    {
      
    }

    public interface IUnitApplierItem
    {
        void ApplyTo(UnitsEntity owner);
        void RemoveFrom(UnitsEntity owner);
    }
}