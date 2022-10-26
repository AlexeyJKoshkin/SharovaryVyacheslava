using System;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using Entitas;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

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
        private readonly CoreGamePlayContext _coreGamePlay;
   
        private readonly IUnitsBuilderFacade _unitBuilder;
        private readonly ICurrentUserProfile _userProfile;
        private readonly IPrepareGameUICommand _prepareGameUiCommand;
        private readonly ICoreLevelBuilder _coreLevelBuilder;
        private readonly ICoreLevelDataInfrastructure _coreLevelDataInfrastructure;
        private readonly ILevelWaveLoader _levelWaveProvider;
        private readonly CoreGameBehaviourNode _coreGameBehaviourNode;
        private readonly IMobAtLevelDirector _mobAtLevelDirector;

        public LevelCreationOperation(IUnitsBuilderFacade unitBuilder,
                                      ICurrentUserProfile userProfile,
                                      ICoreLevelBuilder coreLevelBuilder,
                                      ICoreLevelDataInfrastructure coreLevelDataInfrastructure,
                                      CoreGameBehaviourNode coreGameBehaviourNode,
                                      Contexts contexts,
                                      IMobAtLevelDirector mobAtLevelDirector, IPrepareGameUICommand prepareGameUiCommand,
                                      ILevelWaveLoader levelWaveProvider)
        {
            _unitBuilder       = unitBuilder;
            _userProfile       = userProfile;
            _coreLevelBuilder  = coreLevelBuilder;
            _coreLevelDataInfrastructure = coreLevelDataInfrastructure;
            this._coreGameBehaviourNode = coreGameBehaviourNode;
            _coreGamePlay = contexts.coreGamePlay;
            _mobAtLevelDirector = mobAtLevelDirector;
            _prepareGameUiCommand = prepareGameUiCommand;
            _levelWaveProvider = levelWaveProvider;
        }

        public IBehaviourTreeNode CreateLevel()
        {
            // создаем карту
            _coreLevelBuilder.BuildLevel(_coreLevelDataInfrastructure);
            // создаем всю остальную хуйню (спавнер мобов, карту)
            _levelWaveProvider.InitWaves(_coreLevelDataInfrastructure.PackLevels);
            _mobAtLevelDirector.StartWaveImmediate();
            CreatePlayer();

            _prepareGameUiCommand.PrepareUIStartGame();

            return _coreGameBehaviourNode;
        }

        private void CreatePlayer()
        {
            var selectedHero   = _userProfile.CurrentHeroData;
            var selectedWeapon = _userProfile.CurrentWeaponData;
            CreateCorePlayer();
            _unitBuilder.CreatePlayer(selectedHero, selectedWeapon);
        }

        private void CreateCorePlayer()
        {
            _coreGamePlay.isPlayer = true;
            //вот это по идее надо будет грузить отдельно
            _coreGamePlay.playerEntity.AddExperience(0);
            _coreGamePlay.playerEntity.AddGold(0);
            _coreGamePlay.playerEntity.AddGems(0);
        }
    }
}