using Entitas;
using static RoyalAxe.EntitasSystems.AnimationEntityActions;
namespace RoyalAxe.EntitasSystems
{
    public class MeleeKnightAnimationSystem : BaseUnitAnimationSystem
    {
        public MeleeKnightAnimationSystem(IContext<RAAnimationEntity> context) : base(context) { }

        public override void Initialize()
        {
            AddSubSystem(GetWith(RAAnimationMatcher.DieTrigger), PlayDie);
            AddSubSystem(GetWith(RAAnimationMatcher.AttackTrigger), AnimationEntityActions.PlayAttackMeleeMob);
        }
    }
}