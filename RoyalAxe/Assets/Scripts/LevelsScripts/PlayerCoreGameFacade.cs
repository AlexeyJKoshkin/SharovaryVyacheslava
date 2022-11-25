using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CharacterStat;
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
        private readonly IDataStorage _dataStorage;
        public PlayerCoreGameFacade(CoreGamePlayContext coreGamePlay, IUnitsBuilderFacade unitBuilder,
                                    ICurrentUserProgressProfileFacade currentUserProgressProfileFacade, IDataStorage dataStorage)
        {
            _coreGamePlay = coreGamePlay;
            _unitBuilder  = unitBuilder;
            _currentUserProgressProfileFacade = currentUserProgressProfileFacade;
            _dataStorage = dataStorage;
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

            var weapon = _dataStorage.ById<WeaponsSkillConfigDef>(weaponRecord.Id).GetByLevel(weaponRecord.Level);
            return new UnitBlueprint(heroRecord)
            {
                Stats = _dataStorage.ById<StatCollection>(heroRecord.Id).GetByLevel(heroRecord.Level),
                ActiveSkill = new SkillBlueprint(weaponRecord)
                {
                    DamageData = weapon.damage,
                    RangeData  = weapon.rangeParams
                }
            };
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