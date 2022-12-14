using Entitas;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.CoreLevel 
{
    public class LevelExperienceSystem : RAReactiveSystem<CoreGamePlayEntity>
    {
        private readonly IShowSelectBuffWindowCommand _showSelectBuffWindowCommand;
        private int NeedExpa = 100;
        
        public LevelExperienceSystem(IContext<CoreGamePlayEntity> context, IShowSelectBuffWindowCommand showSelectBuffWindow) : base(context)
        {
            _showSelectBuffWindowCommand = showSelectBuffWindow;
        }

        protected override ICollector<CoreGamePlayEntity> GetTrigger(IContext<CoreGamePlayEntity> context)
        {
            return context.CreateCollector(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayMatcher.Experience, CoreGamePlayMatcher.Player));
        }

        protected override bool Filter(CoreGamePlayEntity entity)
        {
            return true;
        }

        protected override void Execute(CoreGamePlayEntity e)
        {
            if (e.experience.Value >= NeedExpa)
            {
                var delta = e.experience.Value - NeedExpa;
                e.ReplaceExperience(delta);
                _showSelectBuffWindowCommand.ExecuteCommand();
            }
        }
    }
}