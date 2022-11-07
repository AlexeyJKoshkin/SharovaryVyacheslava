using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public interface ILevelCreation
    {
        //тут создаем и подготавливаем данные для работы уровня
        IBehaviourTreeNode CreateLevel();

        void StartLevel();
    }


    //todo: переписать используя декораторы. Надо ждать всякие анимации, колбэки всякое такое. 
    public class LevelCreationOperation : ILevelCreation
    {
        private readonly IPrepareGameUICommand _prepareGameUiCommand;
        private readonly ICoreLevelBuilder _coreLevelBuilder;
        private readonly ICoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        private readonly ILevelWaveLoader _levelWaveProvider;
        private readonly CoreGameBehaviourNode _coreGameBehaviourNode;
        private readonly IMobSpawnOperation _mobSpawnOperation;
        private readonly IMobSpawnTimer _mobSpawnTimer;
        private readonly IPlayerCoreGameFacade _playerCoreGameFacade;

        public LevelCreationOperation(ICoreLevelBuilder coreLevelBuilder,
                                      ICoreLevelDataInfrastructure coreLevelDataInfrastructure,
                                      CoreGameBehaviourNode coreGameBehaviourNode,
                                      IMobSpawnOperation mobSpawnOperation,
                                      IMobSpawnTimer mobSpawnTimer,
                                      IPrepareGameUICommand prepareGameUiCommand,
                                      ILevelWaveLoader levelWaveProvider, IPlayerCoreGameFacade playerCoreGameFacade)
        {
            _coreLevelBuilder            = coreLevelBuilder;
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            this._coreGameBehaviourNode  = coreGameBehaviourNode;
            _mobSpawnOperation = mobSpawnOperation;
            _mobSpawnTimer = mobSpawnTimer;

            _prepareGameUiCommand = prepareGameUiCommand;
            _levelWaveProvider    = levelWaveProvider;
            _playerCoreGameFacade = playerCoreGameFacade;
        }

        public void StartLevel()
        {
            _levelWaveProvider.NextWave(); // по факту грузится первый уровень
            _mobSpawnTimer.StartMobTimer();// запускаем таймер
            _mobSpawnOperation.SpawnMobs();// первую волну спавним на старте

        }

        IBehaviourTreeNode ILevelCreation.CreateLevel()
        {
            // создаем карту
            _coreLevelBuilder.BuildLevel(_coreLevelDataInfrastructure);
            // создаем все остальное (спавнер мобов, карту)
            _levelWaveProvider.Init(_coreLevelDataInfrastructure);
            _playerCoreGameFacade.CreatePlayer();
            _prepareGameUiCommand.PrepareUIStartGame();
            return _coreGameBehaviourNode;
        }
    }
}