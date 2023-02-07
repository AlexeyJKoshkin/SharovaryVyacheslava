using Core.Data.Provider;
using Entitas;
using RoyalAxe.EntitasSystems;
using RoyalAxe.UI;

namespace RoyalAxe.CoreLevel 
{
    public class LevelExperienceSystem : RAReactiveSystem<CoreGamePlayEntity>, IInitializeSystem
    {
        private readonly IDataStorage _dataStorage;
        private IUIScenarioExecutor _showSelectBuffWindowCommand;

        private LevelCounter _levelCounter = new LevelCounter();
        
        private ExpaOnLevelData _expaOnLevelData;
        
        public LevelExperienceSystem(IContext<CoreGamePlayEntity> context,
                                     IDataStorage dataStorage,
                                     IUIScenarioExecutor showSelectBuffWindow) : base(context)
        {
            _dataStorage = dataStorage;
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
            if (_levelCounter.Check(e))
            {
                _showSelectBuffWindowCommand.ExecuteSelectBufUIScenario();
            }
        }

        public void Initialize()
        {
            _levelCounter.LoadData(_dataStorage.First<ExpaOnLevelData>());
        }
        
        class LevelCounter
        {
            public int Level { get; private set; }
            private int[] ExpaArray;
            private int _needExpa;

            public void LoadData(ExpaOnLevelData first)
            {
                ExpaArray = first.NeedUSerExperience;
                _needExpa = ExpaArray[0];
            }

            public bool Check(CoreGamePlayEntity e)
            {
                if (e.earnedExperience.Value >= _needExpa)
                {
                    var delta = e.earnedExperience.Value - _needExpa;
                    e.ReplaceEarnedExperience(delta);
                    Level++;
                    _needExpa = ExpaArray[Level];
                    return true;
                }

                return false;
            }
        }
    }
}