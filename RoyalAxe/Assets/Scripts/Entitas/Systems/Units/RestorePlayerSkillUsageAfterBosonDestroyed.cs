using Entitas;

namespace RoyalAxe.EntitasSystems {
    public class RestorePlayerSkillUsageAfterBosonDestroyed : RAReactiveSystem<UnitsEntity>
    {
        private readonly UnitsContext _unitsContext;

        public RestorePlayerSkillUsageAfterBosonDestroyed(UnitsContext context) : base(context)
        {
            _unitsContext = context;
        }

        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            return context.CreateCollector(UnitsMatcher.AllOf(UnitsMatcher.PlayerBoson, UnitsMatcher.Boson, UnitsMatcher.DestroyUnit).NoneOf(UnitsMatcher.AdditionalBoson).Added());
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(UnitsEntity e)
        {
            var skill = _unitsContext.playerEntity.unitActiveSkill.SkillEntity;
            var usage = skill.useCounterSkill;
            skill.ReplaceUseCounterSkill(usage.CurrentValue + skill.priceUseSkill.Price, usage.MaxValue);
        }
    }
}