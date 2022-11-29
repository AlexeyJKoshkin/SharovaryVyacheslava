using Entitas;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.EntitasSystems
{
    /// <summary>
    ///     мобы наносят урон
    /// </summary>
    public class MeleeAttackCalculationSystem : RAReactiveSystem<UnitsEntity>, IGamePlaySceneSystem
    {
        public MeleeAttackCalculationSystem(IContext<UnitsEntity> context) : base(context)
        {
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(UnitsMatcher.AllOf(UnitsMatcher.PossibleTargets, UnitsMatcher.Damage)
                                                       .NoneOf(UnitsMatcher.DestroyUnit).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            entity.possibleTargets.Collection.RemoveAll(o => !o.isEnabled || o.isDestroyUnit || o.isDeadUnit);
            return entity.possibleTargets.Count > 0;
        }

        protected override void Execute(UnitsEntity e)
        {
            var target = e.possibleTargets.Collection[0];
            InfluenceAllDamage(e,target); // нанесли урон. потом будем что-то возращать
            e.possibleTargets.Remove(target);
        }
        
        void InfluenceAllDamage(UnitsEntity attacker, UnitsEntity target)
        {
            if(attacker == null || !attacker.hasDamage) return;

            foreach (var damageOperation in attacker.damage)
            {
                damageOperation.Apply(attacker, target);
            }
        }
    }
}