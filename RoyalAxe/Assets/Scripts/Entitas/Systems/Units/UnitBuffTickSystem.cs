using System.Linq;
using Entitas;
using FluentBehaviourTree;


namespace RoyalAxe.EntitasSystems
{

    public class UnitBuffTickSystem : IExecuteSystem
    {
        private readonly IGroup<UnitsEntity> _unitsWithBuffs;
        public UnitBuffTickSystem(UnitsContext unitsContext)
        {
            _unitsWithBuffs = unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.ActiveUnitBuff));
        }

        public void Execute()
        {
            foreach (var buff in _unitsWithBuffs.AsEnumerable()
                                                 .Select(o=> o.activeUnitBuff)
                                                 .Where(o=> o.Count > 0)
                                                 .SelectMany(o=> o.Collection))
            {
                buff.Execute(TimeData.Last);
            }
        }

   
    }
}
