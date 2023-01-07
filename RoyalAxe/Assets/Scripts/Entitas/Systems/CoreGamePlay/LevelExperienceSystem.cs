using Entitas;
using RoyalAxe.EntitasSystems;
using RoyalAxe.UI;

namespace RoyalAxe.CoreLevel 
{
    public class LevelExperienceSystem : RAReactiveSystem<CoreGamePlayEntity>
    {
        private IUIScenarioExecutor _showSelectBuffWindowCommand;
        private int NeedExpa = 100;
        
        public LevelExperienceSystem(IContext<CoreGamePlayEntity> context, IUIScenarioExecutor showSelectBuffWindow) : base(context)
        {
            _showSelectBuffWindowCommand = showSelectBuffWindow;
        }

        protected override ICollector<CoreGamePlayEntity> GetTrigger(IContext<CoreGamePlayEntity> context)
        {
            return context.CreateCollector(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayMatcher.EarnedExperience, CoreGamePlayMatcher.Player));
        }

        protected override bool Filter(CoreGamePlayEntity entity)
        {
            return true;
        }

        protected override void Execute(CoreGamePlayEntity e)
        {
            if (e.earnedExperience.Value >= NeedExpa)
            {
                var delta = e.earnedExperience.Value - NeedExpa;
                e.ReplaceEarnedExperience(delta);
                _showSelectBuffWindowCommand.ExecuteSelectBufUIScenario();
            }
        }
    }
}