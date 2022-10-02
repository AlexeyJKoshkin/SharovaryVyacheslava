using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class MeleeKnightAnimationSystem : BaseUnitAnimationSystem
    {
        public MeleeKnightAnimationSystem(IContext<RAAnimationEntity> context) : base(context) { }

        public override void Initialize()
        {
            AddSubSystem(GetWith(RAAnimationMatcher.AttackTrigger), AnimationEntityActions.PlayAttackMeleeMob);
        }
    }
}