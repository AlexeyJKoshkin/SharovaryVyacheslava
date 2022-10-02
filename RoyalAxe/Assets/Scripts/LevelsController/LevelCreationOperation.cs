using System;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using Entitas;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public interface ILevelCreation
    {
        void CreateLevel();
    }

    //todo: переписать используя декораторы. Надо ждать всякие анимации, колбэки всякое такое. 
    public class LevelCreationOperation : ILevelCreation
    {
        private readonly IDataStorage _dataStorage;
        private readonly CoreGamePlayContext _coreGamePlay;
        private readonly CoreGameSceneUIView _coreGameSceneUiView;
        private readonly IUnitsBuilderFacade _unitBuilder;
        private readonly ICurrentUserProfile _userProfile;
        private readonly Contexts _contexts;
        private readonly ICoreLevelBuilder _coreLevelBuilder;

        public LevelCreationOperation(IUnitsBuilderFacade unitBuilder,
                                      ICurrentUserProfile userProfile,
                                      ICoreLevelBuilder coreLevelBuilder,
                                      IDataStorage dataStorage,
                                      CoreGamePlayContext coreGamePlay,
                                      Contexts contexts, CoreGameSceneUIView coreGameSceneUiView)
        {
            _unitBuilder       = unitBuilder;
            _userProfile       = userProfile;
            _coreLevelBuilder  = coreLevelBuilder;
            _dataStorage       = dataStorage;
            _coreGamePlay = coreGamePlay;
            _contexts = contexts;
            _coreGameSceneUiView = coreGameSceneUiView;
        }

        public void CreateLevel()
        {
            //todo: пока просто отпрвляю все что есть. По идее надо формировать данные исходя из прогресса и игрока и данных из UI 
            var infrastructure = new CoreLevelDataInfrastructure(_dataStorage.All<LevelGeneratorSettings>().ToList());
            // создаем карту
            _coreLevelBuilder.BuildLevel(infrastructure);
            // создаем всю остальную хуйню (спавнер мобов, карту)

            CreatePlayer();

            BindUI();
            
            
       
        }

        private void BindUI()
        {
            _coreGameSceneUiView.InitEntity( _contexts.units.playerEntity);
            _coreGameSceneUiView.InitEntity( _contexts.coreGamePlay.playerEntity);
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