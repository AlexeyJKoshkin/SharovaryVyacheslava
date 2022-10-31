using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel
{
    public interface ILevelCreation
    {
        //тут создаем и подготавливаем данные для работы уровня
        IBehaviourTreeNode CreateLevel();
    }


    //todo: переписать используя декораторы. Надо ждать всякие анимации, колбэки всякое такое. 
    public class LevelCreationOperation : ILevelCreation
    {
        private readonly IPrepareGameUICommand _prepareGameUiCommand;
        private readonly ICoreLevelBuilder _coreLevelBuilder;
        private readonly ICoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        private readonly ILevelWaveLoader _levelWaveProvider;
        private readonly CoreGameBehaviourNode _coreGameBehaviourNode;
        private readonly IMobAtLevelDirector _mobAtLevelDirector;
        private readonly IPlayerCoreGameFacade _playerCoreGameFacade;

        public LevelCreationOperation(ICoreLevelBuilder coreLevelBuilder,
                                      ICoreLevelDataInfrastructure coreLevelDataInfrastructure,
                                      CoreGameBehaviourNode coreGameBehaviourNode,
                                      IMobAtLevelDirector mobAtLevelDirector, IPrepareGameUICommand prepareGameUiCommand,
                                      ILevelWaveLoader levelWaveProvider, IPlayerCoreGameFacade playerCoreGameFacade)
        {
            _coreLevelBuilder            = coreLevelBuilder;
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            this._coreGameBehaviourNode  = coreGameBehaviourNode;

            _mobAtLevelDirector   = mobAtLevelDirector;
            _prepareGameUiCommand = prepareGameUiCommand;
            _levelWaveProvider    = levelWaveProvider;
            _playerCoreGameFacade = playerCoreGameFacade;
        }

        public IBehaviourTreeNode CreateLevel()
        {
            // создаем карту
            _coreLevelBuilder.BuildLevel(_coreLevelDataInfrastructure);
            // создаем всю остальную хуйню (спавнер мобов, карту)
            _levelWaveProvider.InitWaves(_coreLevelDataInfrastructure.PackLevels);
            _mobAtLevelDirector.StartWaveImmediate();
            _playerCoreGameFacade.CreatePlayer();
            _prepareGameUiCommand.PrepareUIStartGame();

            return _coreGameBehaviourNode;
        }
    }
}