using Entitas;
using RoyalAxe.EntitasSystems;

namespace RoyalAxe.CoreLevel 
{
    public class LevelExperienceSystem : RAReactiveSystem<CoreGamePlayEntity>
    {
        private readonly IShowBuffCommand _showBuffCommand;
        private int NeedExpa = 50;
        
        public LevelExperienceSystem(IContext<CoreGamePlayEntity> context, IShowBuffCommand showBuffCommand) : base(context)
        {
            _showBuffCommand = showBuffCommand;
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
                _showBuffCommand.DoShowExpBuffs();
            }

            //по идее проверяем количество опыта. и если надо то переключаемся
        }
    }
}