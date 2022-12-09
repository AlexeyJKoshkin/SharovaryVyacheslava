using System.Collections.Generic;
using Entitas;
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
            return context.CreateCollector(UnitsMatcher.AllOf(UnitsMatcher.PossibleTargets).NoneOf(UnitsMatcher.DestroyUnit).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            if (!entity.isEnabled) return false;
            entity.possibleTargets.Collection.RemoveAll(o => !o.isEnabled || o.isDestroyUnit || o.isDeadUnit);
            return entity.possibleTargets.Count > 0;
        }

        protected override void Execute(UnitsEntity e)
        {
            foreach (var damageOperation in GetInfluenceApplier(e)) //обходим каждую пачку урона
            {
                foreach (var target in e.possibleTargets)
                {
                    damageOperation.AttackTarget(target); // нанесли урон. 
                }
              
            }
            e.possibleTargets.Collection.Clear();
            e.ReplacePossibleTargets(e.possibleTargets.Collection);
            //todo: переделать. Анимация удара - должна происходить в месте удара
            e.unitAnimationEntity.AnimationEntity.isAttackTrigger = true;
            
        }

        private IEnumerable<IWeaponItem> GetInfluenceApplier(UnitsEntity attacker)
        {
            if (attacker.hasMainDamage)
                 yield return attacker.mainDamage.Influence;
            if (!attacker.hasOtherDamage) yield break;
            

            foreach (var damage in attacker.otherDamage)
            {
                yield return damage;
            }
        }
    }
    
}