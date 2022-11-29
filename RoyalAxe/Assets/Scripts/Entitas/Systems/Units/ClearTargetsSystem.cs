using Entitas;

namespace RoyalAxe.EntitasSystems
{
    /// <summary>
    ///     Очистка спика физ-взаимодействия мобов
    /// </summary>
    public class ClearTargetsSystem : RAReactiveSystem<UnitsEntity>
    {
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(UnitsMatcher.AllOf(UnitsMatcher.Mob, UnitsMatcher.PossibleTargets).NoneOf(UnitsMatcher.DestroyUnit).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return entity.possibleTargets.Count > 0;
        }

        protected override void Execute(UnitsEntity e)
        {
            e.possibleTargets.Clear();
        }

        public ClearTargetsSystem(IContext<UnitsEntity> context) : base(context) { }
    }

    public class ClearPhysicalInteractionSystem : RAReactiveSystem<UnitsEntity>
    {
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(UnitsMatcher.AllOf(UnitsMatcher.EnterPhysicInteraction).NoneOf(UnitsMatcher.DestroyUnit).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return entity.isEnabled;
        }

        protected override void Execute(UnitsEntity e)
        {
            e.enterPhysicInteraction.Collection.Clear();
        }

        public ClearPhysicalInteractionSystem(IContext<UnitsEntity> context) : base(context) { }
    }


   
}