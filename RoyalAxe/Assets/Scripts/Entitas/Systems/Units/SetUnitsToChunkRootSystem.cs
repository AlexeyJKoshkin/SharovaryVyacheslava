using System.Linq;
using Core;
using Entitas;
using RoyalAxe.CoreLevel;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.GameEntitas
{
    public class SetUnitsToChunkRootSystem : RAReactiveSystem<UnitsEntity>
    {
        private readonly ILevelAdapter _levelAdapter;
     
        public SetUnitsToChunkRootSystem(IContext<UnitsEntity> context,ILevelAdapter levelAdapter) : base(context)
        {
            _levelAdapter = levelAdapter;
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(Matcher<UnitsEntity>.AllOf(UnitsMatcher.UnitsView).NoneOf(UnitsMatcher.Player).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return entity.hasUnitsView;
        }

        protected override void Execute(UnitsEntity unit)
        {
            var unitRoot = unit.unitsView.RootTransform;
            unitRoot.SetParent(_levelAdapter.ChunkRoot, true);       
        }
    }
}