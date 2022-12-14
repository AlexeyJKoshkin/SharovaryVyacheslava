using Core;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public class LoadNextWaveNode : SequenceNode
    {
        private readonly IWizardAtLevelFacade _wizardAtLevelFacade;
        private readonly ILevelWaveLoader _levelWaveLoader;
        private readonly CoreGamePlayEntity _coreGamePlay;
        private readonly IRoyalAxeCoreMap _royalAxeCoreMap;
        private readonly IMobSpawnFacade _mobSpawnFacade;

        public LoadNextWaveNode(CoreGamePlayContext coreGamePlay,
                                IWizardAtLevelFacade wizardAtLevelFacade,
                                ILevelWaveLoader levelWaveLoader, IMobSpawnFacade mobSpawnFacade, IRoyalAxeCoreMap royalAxeCoreMap) : base("Грузим следующий волну")
        {
            _wizardAtLevelFacade = wizardAtLevelFacade;
            _levelWaveLoader = levelWaveLoader;
            _mobSpawnFacade = mobSpawnFacade;
            _royalAxeCoreMap = royalAxeCoreMap;
            _coreGamePlay = coreGamePlay.levelWaveEntity;

            new BehaviourTreeBuilder().Sequence(this)
                                      .Condition("Волна закончилась?",
                                                 dt => _coreGamePlay.isWaveFinished && _royalAxeCoreMap.CurrentMobAmount == 0)
                                      .Do("NextWave", TryLoadNext)
                                      .Condition("Check Has Wizard",
                                                 dt => _coreGamePlay.hasWizardShopReady)
                                      .Do("Spawn Wizard", SpawnWizard)
                                      .End().Build();
        }

        private BehaviourTreeStatus SpawnWizard(TimeData arg)
        {
            _wizardAtLevelFacade.SpawnWizard(()=>HLogger.LogError("Показали волшебника")); 
            HLogger.LogError("Спавн волшебника");
            return BehaviourTreeStatus.Success;
        }

        private BehaviourTreeStatus TryLoadNext(TimeData arg)
        {
            _mobSpawnFacade.StopSpawn();
            if (_levelWaveLoader.NextWave()) // пробуем загрузить следующую волну один раз
            {
                HLogger.LogError("Load Next wave");
                return BehaviourTreeStatus.Success;
            }
            return BehaviourTreeStatus.Failure;
        }
    }
}
