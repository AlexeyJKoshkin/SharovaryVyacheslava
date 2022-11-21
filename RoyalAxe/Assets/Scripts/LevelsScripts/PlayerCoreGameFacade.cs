using Core.UserProfile;
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
        private readonly ICurrentUserProgressProfileFacade _currentUserProgressProfileFacade;

        private readonly IUnitsBuilderFacade _unitBuilder;
        public PlayerCoreGameFacade(CoreGamePlayContext coreGamePlay, IUnitsBuilderFacade unitBuilder,  GameRootLoopContext gameRootLoopContext,
                                    ICurrentUserProgressProfileFacade currentUserProgressProfileFacade)
        {
            _coreGamePlay = coreGamePlay;
            _unitBuilder  = unitBuilder;
            _currentUserProgressProfileFacade = currentUserProgressProfileFacade;
        }

        public void CreatePlayer()
        {
            var selectedHero = _currentUserProgressProfileFacade.Get<IUserProfileHeroesProgress>().CurrentHero;
            var selectedWeapon = _currentUserProgressProfileFacade.Get<IUserProfileWeaponsProgress>().GetWeaponProgress(selectedHero.EquipWeapon);
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