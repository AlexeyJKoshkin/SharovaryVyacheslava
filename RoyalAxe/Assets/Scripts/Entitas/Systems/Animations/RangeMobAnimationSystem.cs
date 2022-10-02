using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class RangeMobAnimationSystem : BaseUnitAnimationSystem, ITearDownSystem
    {
        private IGroup<RAAnimationEntity> _sitAnimations;

        public RangeMobAnimationSystem(IContext<RAAnimationEntity> context) : base(context)
        {
            var matcher = Matcher<RAAnimationEntity>.AllOf(RAAnimationMatcher.Animator, RAAnimationMatcher.IsSit).NoneOf(RAAnimationMatcher.Run);
            _sitAnimations = context.GetGroup(matcher);
        }

        public override void Initialize()
        {
            _sitAnimations.OnEntityUpdated += SitAnimationsOnOnEntityUpdated;
            //AddSubSystem(matcher, AnimationEntityActions.PlaySitAnimation, GroupEvent.AddedOrRemoved);
            AddSubSystem(GetWith(RAAnimationMatcher.AttackTrigger), AnimationEntityActions.PlayAttackGunnerMob);
        }


        public override void TearDown()
        {
            _sitAnimations.OnEntityUpdated -= SitAnimationsOnOnEntityUpdated;
        }

        private void SitAnimationsOnOnEntityUpdated(IGroup<RAAnimationEntity> group, RAAnimationEntity entity, int index, IComponent previouscomponent, IComponent newcomponent)
        {
            AnimationEntityActions.PlaySitAnimation(entity, entity.animator.Controller);
        }

        private void SitAnimationsOnOnEntityAdded(IGroup<RAAnimationEntity> group, RAAnimationEntity entity, int index, IComponent component)
        {
            AnimationEntityActions.PlaySitAnimation(entity, entity.animator.Controller);
        }
    }
}