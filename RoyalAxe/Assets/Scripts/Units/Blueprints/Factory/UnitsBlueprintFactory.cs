using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel 
{
    public class UnitsBlueprintFactory : IUnitsBlueprintsFactory
    {
        private readonly IDataStorage _dataStorage;
        private readonly IItemsBlueprintsFactory _itemsBlueprintsFactory;
        
        
        public UnitsBlueprintFactory(IDataStorage dataStorage, IItemsBlueprintsFactory itemsBlueprints)
        {
            _dataStorage = dataStorage;
            _itemsBlueprintsFactory = itemsBlueprints;
        }

        public MobBlueprint CreateMobBluePrint(WeaponsSkillConfigDef weaponData, StatCollection mobStatCollection, int level, string id)
        {
            return new MobBlueprint(id, level)
            {
                Stats = mobStatCollection.GetByLevel(level),
                MainItemBluePrint = _itemsBlueprintsFactory.CreateMainWeapon(weaponData, level)
            };
        }
        
        public IDictionary<int, MobBlueprint> CreateMobBluePrints(string mobId, IEnumerable<MobAtLevelData> mobs)
        {
            var mobStatCollection = _dataStorage.ById<StatCollection>(mobId);
            var weaponData        = _dataStorage.ById<WeaponsSkillConfigDef>(mobId);
            
            var blueprints = new Dictionary<int, MobBlueprint>();
            foreach (var levelGroup in mobs.GroupBy(o=> o.Level))
            {
                var level        = levelGroup.Key;
                var mobBluePrint = CreateMobBluePrint(weaponData, mobStatCollection, level, mobId);
                blueprints.Add(level, mobBluePrint);
            }

            return blueprints;
        }

        public UnitBlueprint CreatePlayerBluePrint(HeroProgressData heroRecord, SaveEntityRecord weaponRecord)
        {
            return new UnitBlueprint(heroRecord)
            {
                Stats = _dataStorage.ById<StatCollection>(heroRecord.Id).GetByLevel(heroRecord.Level),
               MainItemBluePrint  = _itemsBlueprintsFactory.CreateMainWeapon(weaponRecord.Id, weaponRecord.Level)
            };
        }
    }
}