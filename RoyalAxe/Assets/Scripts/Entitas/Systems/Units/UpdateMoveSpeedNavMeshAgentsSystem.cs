using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class UpdateMoveSpeedNavMeshAgentsSystem : RAReactiveSystem<UnitsEntity>
    {
        public UpdateMoveSpeedNavMeshAgentsSystem(IContext<UnitsEntity> context) : base(context) { }
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(Matcher<UnitsEntity>.AllOf(UnitsMatcher.NavMeshAgent, UnitsMatcher.MoveSpeed).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity e)
        {
            e.navMeshAgent.Speed = e.moveSpeed.CurrentValue;
        }
    }
}
