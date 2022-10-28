using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    /// <summary>
    ///     Какой-то баф, который висит на юните
    /// </summary>
    public interface IEntityBuff : IBehaviourTreeNode
    {
        void ApplyTo(UnitsEntity owner);
        void RemoveFrom(UnitsEntity owner, bool isSilent = false);
    }
}