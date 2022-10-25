using Core;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public class LoadNextWaveNode : SequenceNode
    {
        private readonly IWizardAtLevelFacade _wizardAtLevelFacade;
        private readonly ILevelWaveLoader _levelWaveLoader;
        private readonly CoreGamePlayEntity _coreGamePlay;

        public LoadNextWaveNode(CoreGamePlayContext coreGamePlay,
                                IWizardAtLevelFacade wizardAtLevelFacade,
                                ILevelWaveLoader levelWaveLoader) : base("Грузим следующий волну")
        {
            _wizardAtLevelFacade = wizardAtLevelFacade;
            _levelWaveLoader = levelWaveLoader;
            _coreGamePlay = coreGamePlay.levelWaveEntity;

            new BehaviourTreeBuilder().Sequence(this)
                                      .Condition("Волна закончилась?",
                                                 dt => _coreGamePlay.isWaveFinished)
                                      .Do("NextWave", TryLoadNext)
                                      .Condition("Check Has Wizard",
                                                 dt => _coreGamePlay.isWizardShopReady)
                                      .Do("Spawn Wizard", SpawnWizard)
                                      .End().Build();
        }

        private BehaviourTreeStatus SpawnWizard(TimeData arg)
        {
            _wizardAtLevelFacade.SpawnWizard(()=> _coreGamePlay.isWaveMobReady = true); 
            HLogger.LogError("Спавн волшебника");
            return BehaviourTreeStatus.Success;
        }

        private BehaviourTreeStatus TryLoadNext(TimeData arg)
        {
            if (_levelWaveLoader.NextWave()) // пробуем загрузить следующую волну один раз
            {
                HLogger.LogError("Load Next wave");
            }
            else
            {
                HLogger.LogError("Волны закончились");
            }

            return BehaviourTreeStatus.Success;
        }
    }
}
