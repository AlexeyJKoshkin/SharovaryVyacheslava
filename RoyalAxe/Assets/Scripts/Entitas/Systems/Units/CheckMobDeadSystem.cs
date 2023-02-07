using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class CheckUnitDeadSystem : RAReactiveSystem<UnitsEntity>, IGamePlaySceneSystem
    {

        public CheckUnitDeadSystem(UnitsContext context, CoreGamePlayContext coreGamePlayContext) : base(context)
        {
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            var healthUnitMatcher = Matcher<UnitsEntity>.AllOf(UnitsMatcher.Health,
                                                              UnitsMatcher.UnitsView,
                                                              UnitsMatcher.UnitPhysicCollider,
                                                              UnitsMatcher.UnitAnimationEntity);
            var replaceTrigger = new TriggerOnEvent<UnitsEntity>(healthUnitMatcher, GroupEvent.Added);
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
        }
    }

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
            var deadMobRewardTrigger = Matcher<UnitsEntity>.AllOf(UnitsMatcher.DeadUnit,
                                                              UnitsMatcher.MobDeathReward);
            var replaceTrigger = new TriggerOnEvent<UnitsEntity>(deadMobRewardTrigger, GroupEvent.Added);
            return context.CreateCollector(replaceTrigger);
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity e)
        {
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