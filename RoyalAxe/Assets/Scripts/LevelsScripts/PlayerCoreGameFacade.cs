using Core.UserProfile;
using RoyalAxe.CoreLevel;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public interface IPlayerCoreGameFacade
    {
        void CreatePlayer();
    }
    
    public class PlayerCoreGameFacade : IPlayerCoreGameFacade
    {
        private readonly CoreGamePlayContext _coreGamePlay;
        private readonly GameRootLoopContext _gameRootLoopContext;
   
        private readonly IUnitsBuilderFacade _unitBuilder;
        public PlayerCoreGameFacade(CoreGamePlayContext coreGamePlay, IUnitsBuilderFacade unitBuilder,  GameRootLoopContext gameRootLoopContext)
        {
            _coreGamePlay = coreGamePlay;
            _unitBuilder  = unitBuilder;
            _gameRootLoopContext = gameRootLoopContext;
        }

        public void CreatePlayer()
        {
            var current = _gameRootLoopContext.userProgressEntity;
            var selectedHero = current.userCurrentHeroProgress.Progress;
            var selectedWeapon = current.userCurrentWeaponProgress.Progress;
            CreateCorePlayer();
            _unitBuilder.CreatePlayer(selectedHero, selectedWeapon);
        }

        private void CreateCorePlayer()
        {
            if(!_coreGamePlay.isPlayer)
                _coreGamePlay.isPlayer = true;
            //вот это по идее надо будет грузить отдельно
            _coreGamePlay.playerEntity.ReplaceExperience(0);
            _coreGamePlay.playerEntity.ReplaceGold(0);;
            _coreGamePlay.playerEntity.ReplaceGems(0);
        }
    }
}