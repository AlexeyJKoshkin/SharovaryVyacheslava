using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;
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
        private readonly IBluePrintsFactoryStorage _bluePrintsFactoryStorage;
        public PlayerCoreGameFacade(CoreGamePlayContext coreGamePlay, IUnitsBuilderFacade unitBuilder,
                                    ICurrentUserProgressProfileFacade currentUserProgressProfileFacade, IBluePrintsFactoryStorage bluePrintsFactoryStorage)
        {
            _coreGamePlay = coreGamePlay;
            _unitBuilder  = unitBuilder;
            _currentUserProgressProfileFacade = currentUserProgressProfileFacade;
            _bluePrintsFactoryStorage = bluePrintsFactoryStorage;
        }

        public void CreatePlayer()
        {
            var playerBluePrint = CreatePlayerBluePrint();
            CreateCorePlayer();
            _unitBuilder.CreatePlayer(playerBluePrint);
        }

        private UnitBlueprint CreatePlayerBluePrint()
        {
            var heroRecord   = _currentUserProgressProfileFacade.HeroesProgress.CurrentHero;
            SaveEntityRecord weaponRecord = GetEquippedWeapon();
            return _bluePrintsFactoryStorage.Units.CreatePlayerBluePrint(heroRecord, weaponRecord);

            
        }

        private SaveEntityRecord GetEquippedWeapon()
        {
            var equipment = _currentUserProgressProfileFacade.InventoryProgress.Equipment;
            var weapon = _currentUserProgressProfileFacade.WeaponProgress.GetWeaponProgress(equipment.EquippedWeaponId);
            return weapon;
        }

        private void CreateCorePlayer()
        {
            if(!_coreGamePlay.isPlayer)
                _coreGamePlay.isPlayer = true;
            
            // указываем 0. Потому, что в UI показыве
            _coreGamePlay.playerEntity.ReplaceEarnedExperience(0);
            _coreGamePlay.playerEntity.ReplaceEarnedGold(0);;
            _coreGamePlay.playerEntity.ReplaceEarnedGems(0);
        }
    }
}