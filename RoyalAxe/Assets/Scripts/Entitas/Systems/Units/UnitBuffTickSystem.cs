using System.Linq;
using Entitas;
using FluentBehaviourTree;


namespace RoyalAxe.EntitasSystems
{

    public class UnitBuffTickSystem : IExecuteSystem
    {
        private readonly IGroup<UnitsEntity> _unitsWithBuffs;
        private readonly IGroup<SkillEntity> _activeSkills;
        
        public UnitBuffTickSystem(UnitsContext unitsContext, SkillContext skillContext)
        {
            _unitsWithBuffs = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.ActiveUnitBuff));
            _activeSkills = skillContext.GetGroup(Matcher<SkillEntity>.AllOf(SkillMatcher.BuffBehaviour));
        }

        public void Execute()
        {
            var buffs = _activeSkills.GetEntities();

            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i].buffBehaviour.BehaviourTreeNode.Execute(TimeData.Last);
            }
            
            
            /*foreach (var buff in _unitsWithBuffs.AsEnumerable()
                                                 .Select(o=> o.activeUnitBuff)
                                                 .Where(o=> o.Count > 0)
                                                 .SelectMany(o=> o.Collection)) // собрали все бафы и тикаем каждый из них
            {
                buff.buffBehaviour.BehaviourTreeNode.Execute(TimeData.Last);
            }*/
        }

   
    }
}
