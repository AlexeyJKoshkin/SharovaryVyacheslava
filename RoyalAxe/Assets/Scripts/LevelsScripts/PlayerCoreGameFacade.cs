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
   
        private readonly IUnitsBuilderFacade _unitBuilder;
        private readonly IUserSaveProfileStorage _userSaveProfileStorage;
        public PlayerCoreGameFacade(CoreGamePlayContext coreGamePlay, IUnitsBuilderFacade unitBuilder,  IUserSaveProfileStorage userSaveProfileStorage)
        {
            _coreGamePlay = coreGamePlay;
            _unitBuilder  = unitBuilder;
            _userSaveProfileStorage = userSaveProfileStorage;
        }

        public void CreatePlayer()
        {
            var current = _userSaveProfileStorage.Current;
            var selectedHero = current.LoadCurrentHero();
            var selectedWeapon = current.LoadCurrentWeapon();
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