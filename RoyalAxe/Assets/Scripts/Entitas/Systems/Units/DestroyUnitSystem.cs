using Core;
using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class DestroyUnitSystem : RAReactiveSystem<UnitsEntity>, IGamePlaySceneSystem
    {
        public DestroyUnitSystem(IContext<UnitsEntity> context) : base(context) { }
        

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            var healthMobMatcher = Matcher<UnitsEntity>.AllOf(UnitsMatcher.UnitsView, UnitsMatcher.DestroyUnit).NoneOf(UnitsMatcher.Player);
            return context.CreateCollector(healthMobMatcher);
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity e)
        {
            if(e.hasUnitAnimationEntity && e.unitAnimationEntity.AnimationEntity.isEnabled)
                e.unitAnimationEntity.AnimationEntity.Destroy();

            if (e.hasUnitActiveSkill)
            {
                e.unitActiveSkill.SkillEntity.Destroy();
            }
            //по хорошему вьюшку надо вернуть в пул, но пока просто уничтожаем
            var view = e.unitsView.View;
            Object.Destroy(view.gameObject);
            Object.Destroy(view);
            e.Destroy();
        }
    }
}