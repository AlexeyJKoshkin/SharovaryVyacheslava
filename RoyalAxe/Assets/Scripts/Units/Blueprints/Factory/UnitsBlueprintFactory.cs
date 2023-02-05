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

        public MobBlueprint CreateMobBluePrint(MobWeaponSkillConfigDef weaponData, MobUnitJsonData mobStatCollection, int level, string id)
        {
            return new MobBlueprint(id, level)
            {
                Stats = mobStatCollection.GetStatByLevel(level),
                MainItemBluePrint = _itemsBlueprintsFactory.CreateMainWeapon(weaponData, level)
            };
        }
        
        public IDictionary<int, MobBlueprint> CreateMobBluePrints(string mobId, IEnumerable<MobAtLevelData> mobs)
        {
            var mobJson = _dataStorage.ById<MobUnitJsonData>(mobId);
            
            var blueprints = new Dictionary<int, MobBlueprint>();
            foreach (var levelGroup in mobs.GroupBy(o=> o.Level))
            {
                var level        = levelGroup.Key;
                var mobBluePrint = CreateMobBluePrint(mobJson.MobWeaponData, mobJson, level, mobId);
                blueprints.Add(level, mobBluePrint);
            }

            return blueprints;
        }

        public UnitBlueprint CreatePlayerBluePrint(HeroProgressData heroRecord, SaveEntityRecord weaponRecord)
        {
            return new UnitBlueprint(heroRecord)
            {
               Stats = _dataStorage.ById<HeroUnitJsonData>(heroRecord.Id).GetStatByLevel(heroRecord.Level),
               MainItemBluePrint  = _itemsBlueprintsFactory.CreateMainWeapon(weaponRecord.Id, weaponRecord.Level)
            };
        }
    }
}