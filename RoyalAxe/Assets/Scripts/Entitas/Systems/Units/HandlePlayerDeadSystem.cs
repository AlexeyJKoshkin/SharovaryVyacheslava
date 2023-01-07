using System.Collections.Generic;
using Core;
using Core.Installers;
using Entitas;
using RoyalAxe.CoreLevel;
using RoyalAxe.UI;

namespace RoyalAxe.EntitasSystems 
{
    public class HandlePlayerDeadSystem : ReactiveSystem<UnitsEntity>
    {
        private readonly IUIScenarioExecutor _loseLevelExecutor;
  
        public HandlePlayerDeadSystem(IContext<UnitsEntity> context, IUIScenarioExecutor loseLevelUICommand) : base(context)
        {
            _loseLevelExecutor = loseLevelUICommand;

        }
        protected override ICollector<UnitsEntity> GetTrigger(IContext<UnitsEntity> context)
        {
            var healthMobMatcher = Matcher<UnitsEntity>.AllOf(UnitsMatcher.DeadUnit,
                                                              UnitsMatcher.Player,
                                                              UnitsMatcher.UnitsView);
            var replaceTrigger = new TriggerOnEvent<UnitsEntity>(healthMobMatcher, GroupEvent.Added);
            return context.CreateCollector(replaceTrigger);
        }

        protected override bool Filter(UnitsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<UnitsEntity> entities)
        {
            _loseLevelExecutor.ExecuteLoseUIScenario();
        }
    }
}