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
            var selectedHero   = _currentUserProgressProfileFacade.HeroesProgress.CurrentHero;
            var selectedWeapon = _currentUserProgressProfileFacade.WeaponProgress.GetWeaponProgress(selectedHero.EquipWeapon);
            var heroRecord = selectedHero.CharacterRecord;
            var weaponRecord = selectedWeapon.Weapon;

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