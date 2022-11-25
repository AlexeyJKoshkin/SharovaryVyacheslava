using Core;
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
        private readonly IMobSpawnFacade _mobSpawnFacade;
        private readonly IPlayerCoreGameFacade _playerCoreGameFacade;

        public LevelCreationOperation(ICoreLevelBuilder coreLevelBuilder,
                                      ICoreLevelDataInfrastructure coreLevelDataInfrastructure,
                                      CoreGameBehaviourNode coreGameBehaviourNode,
                                      
                                      IMobSpawnFacade mobSpawnFacade,
                                      IPrepareGameUICommand prepareGameUiCommand,
                                      ILevelWaveLoader levelWaveProvider, IPlayerCoreGameFacade playerCoreGameFacade)
        {
            _coreLevelBuilder            = coreLevelBuilder;
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            this._coreGameBehaviourNode  = coreGameBehaviourNode;
            
            _mobSpawnFacade = mobSpawnFacade;

            _prepareGameUiCommand = prepareGameUiCommand;
            _levelWaveProvider    = levelWaveProvider;
            _playerCoreGameFacade = playerCoreGameFacade;
        }

        public void StartLevel()
        {
            _levelWaveProvider.NextWave(); // по факту грузится первый уровень
            _mobSpawnFacade.StartSpawnMob();
            

        }

        IBehaviourTreeNode ILevelCreation.CreateLevel()
        {
            HLogger.LogCoreLevel($"CreateLevel {_coreLevelDataInfrastructure.ToString()}");
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