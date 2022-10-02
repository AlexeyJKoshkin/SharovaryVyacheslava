using System.Collections.Generic;
using Core;
using Core.Installers;
using Entitas;

namespace RoyalAxe.EntitasSystems 
{
    public class HandlePlayerDeadSystem : ReactiveSystem<UnitsEntity>
    {
        private readonly IRoyalAxePauseSystemSwitcher _pauseSystemSwitcher;
        public HandlePlayerDeadSystem(IContext<UnitsEntity> context, IRoyalAxePauseSystemSwitcher pauseSystemSwitcher ) : base(context)
        {
            _pauseSystemSwitcher = pauseSystemSwitcher;
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
            var player = entities.SingleEntity();
            HLogger.TempLog("Игрок помер");
            _pauseSystemSwitcher.SetPause();
        }
    }
}