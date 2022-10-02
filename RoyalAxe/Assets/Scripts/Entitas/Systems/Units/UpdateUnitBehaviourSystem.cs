using Entitas;
using FluentBehaviourTree;

namespace RoyalAxe.EntitasSystems
{
    public class UpdateUnitBehaviourSystem : IPauseAbleSystem
    {
        private IGroup<UnitsEntity> _behaviour;

        public UpdateUnitBehaviourSystem(UnitsContext context)
        {
            _behaviour = context.GetGroup(UnitsMatcher.AllOf(UnitsMatcher.UnitsView, UnitsMatcher.UnitBehavior, UnitsMatcher.UnitAnimationEntity));
        }

        public void Execute()
        {
            var all = _behaviour.GetEntities();
            for (int i = 0; i < all.Length; i++) UpdateBehaviour(all[i]);
        }

        private void UpdateBehaviour(UnitsEntity unitsEntity)
        {
            unitsEntity.unitBehavior.Behaviour.Execute(TimeData.Last);
        }
    }
}