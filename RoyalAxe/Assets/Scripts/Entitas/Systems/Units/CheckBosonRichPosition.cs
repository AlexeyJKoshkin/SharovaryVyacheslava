using Entitas;
using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems 
{
    public class CheckBosonRichPosition : RAReactiveSystem<UnitsEntity>
    {
        private readonly UnitsContext _context;

        public CheckBosonRichPosition(UnitsContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            var trigger = UnitsMatcherLibrary.MovingSimpleUnits(UnitsMatcher.Boson).Added();
           
            return context.CreateCollector(trigger);
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return entity.movingToPoint.IsRichPosition(entity.unitsView.RootTransform.position);
        }

        protected override void Execute(UnitsEntity e)
        {
            if (e.hasPlayerBoson)
            {
                if (e.movingToPoint.PointAdapter is FollowUnitPointAdapter followAdapter) // если вернулось в игрока
                {
                    if (followAdapter.Unit.isPlayer)
                    {
                        e.isDestroyUnit = true;
                        e.isDeadUnit    = true;
                        e.RemoveMovingToPoint();
                    }
                }
                else
                {
                    e.ReplaceMovingToPoint(new FollowUnitPointAdapter(_context.playerEntity));
                }
                return;
            }

            if (e.isMob)
            {
                e.isDestroyUnit = true;
                e.isDeadUnit    = true;
            }
        }
    }
}