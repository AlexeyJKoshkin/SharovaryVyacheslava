using Entitas;
using RoyalAxe.Map;

namespace RoyalAxe.EntitasSystems
{
    public class UpdateMoveSpeedNavMeshAgentsSystem : RAReactiveSystem<UnitsEntity>
    {
        private readonly TileCoreMapSettings _settings;
        public UpdateMoveSpeedNavMeshAgentsSystem(IContext<UnitsEntity> context, TileCoreMapSettings settings) : base(context)
        {
            _settings = settings;
        }
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(Matcher<UnitsEntity>.AllOf(UnitsMatcher.NavMeshAgent, UnitsMatcher.MoveSpeed).NoneOf(UnitsMatcher.DestroyUnit).AddedOrRemoved());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity e)
        {
            e.navMeshAgent.Speed = e.moveSpeed.CurrentValue + _settings.ChunkSpeed;
        }
    }
}
