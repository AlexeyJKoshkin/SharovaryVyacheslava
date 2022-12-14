using Entitas;
using static RoyalAxe.EntitasSystems.AnimationEntityActions;

namespace RoyalAxe.EntitasSystems
{
    public class DefaultMobAnimationSystem : BaseUnitAnimationSystem
    {
        public DefaultMobAnimationSystem(IContext<RAAnimationEntity> context) : base(context) { }

        public override void Initialize()
        {
            AddSubSystem(GetWith(RAAnimationMatcher.DieTrigger), PlayDie);

            AddSubSystem(GetWith(RAAnimationMatcher.HitTrigger), PlayHitDamage);
        }
    }
}