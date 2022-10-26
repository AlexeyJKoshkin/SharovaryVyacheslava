using System.Collections.Generic;
using Core;
using Core.Installers;
using Entitas;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.EntitasSystems 
{
    public class HandlePlayerDeadSystem : ReactiveSystem<UnitsEntity>
    {
        private readonly ILoseLevelUICommand _loseLevelUICommand;
  
        public HandlePlayerDeadSystem(IContext<UnitsEntity> context, ILoseLevelUICommand loseLevelUICommand) : base(context)
        {
            _loseLevelUICommand = loseLevelUICommand;

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
            _loseLevelUICommand.ExecuteCommand();
        }
    }
}