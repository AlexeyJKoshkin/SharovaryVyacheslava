using Entitas;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.EntitasSystems;
using UnityEngine;

namespace RoyalAxe.GameEntitas 
{
    public class SetPlayerUnitToChunkRootSystem : RAReactiveSystem<UnitsEntity>
    {
        //todo: возможно вьюшку нельзя передавать напрямую. возможно лучше использовать адаптер или что-то такое.
        private readonly LevelInfrastructureView _levelInfrastructure;
        public SetPlayerUnitToChunkRootSystem(IContext<UnitsEntity> context, LevelInfrastructureView levelInfrastructure) : base(context)
        {
            _levelInfrastructure = levelInfrastructure;
        }
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(Matcher<UnitsEntity>.AllOf(UnitsMatcher.UnitsView, UnitsMatcher.Player).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity player)
        {
            player.unitsView.RootTransform.SetParent(_levelInfrastructure.PlayerStartPoint); // игрок всегда инициализируется в рутовой точке
            player.unitsView.RootTransform.localPosition = Vector3.zero;
            
        }
    }
}