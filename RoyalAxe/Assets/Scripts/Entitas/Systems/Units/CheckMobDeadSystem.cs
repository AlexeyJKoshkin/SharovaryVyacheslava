using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class CheckMobDeadSystem : RAReactiveSystem<UnitsEntity>, IGamePlaySceneSystem
    {
        private CoreGamePlayEntity CorePlayer => _coreGamePlayContext.playerEntity;

        private readonly CoreGamePlayContext _coreGamePlayContext;

        public CheckMobDeadSystem(UnitsContext context, CoreGamePlayContext coreGamePlayContext) : base(context)
        {
            _coreGamePlayContext = coreGamePlayContext;
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            var healthMobMatcher = Matcher<UnitsEntity>.AllOf(UnitsMatcher.Health,
                                                              UnitsMatcher.MobDeathReward,
                                                              UnitsMatcher.UnitsView,
                                                              UnitsMatcher.UnitPhysicCollider,
                                                              UnitsMatcher.UnitAnimationEntity);
            var replaceTrigger = new TriggerOnEvent<UnitsEntity>(healthMobMatcher, GroupEvent.Added);
            return context.CreateCollector(replaceTrigger);
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return entity.health.CurrentValue <= 0;
        }

        protected override void Execute(UnitsEntity e)
        {
            e.isDeadUnit                                       = true;
            e.unitAnimationEntity.AnimationEntity.isDieTrigger = true;
            e.unitPhysicCollider.PhysicCollider.enabled        = false;

            var mobReward = e.mobDeathReward;

            if (mobReward.ExpReward != 0)
                CorePlayer.ReplaceEarnedExperience(CorePlayer.earnedExperience.Value + mobReward.ExpReward);
            if (mobReward.GoldReward != 0)
                CorePlayer.ReplaceEarnedGold(CorePlayer.earnedGold.Value + mobReward.GoldReward);
            if (mobReward.GemReward != 0)
                CorePlayer.ReplaceEarnedGems(CorePlayer.earnedGems.Value + mobReward.GemReward);
        }
    }
}