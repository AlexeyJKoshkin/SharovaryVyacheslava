using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CharacterStat;
using RoyalAxe.Configs;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel 
{
    public class UnitsBlueprintFactory : IUnitsBlueprintsFactory
    {
        private readonly IDataStorage _dataStorage;
        private readonly ISkillBlueprintsFactory _skillBluePrintFactory;
        
        
        public UnitsBlueprintFactory(IDataStorage dataStorage, ISkillBlueprintsFactory skillBluePrintFactory)
        {
            _dataStorage = dataStorage;
            _skillBluePrintFactory = skillBluePrintFactory;
        }

        public MobBlueprint CreateMobBluePrint(WeaponsSkillConfigDef weaponData, StatCollection mobStatCollection, int level, string id)
        {
           
            return new MobBlueprint(id, level)
            {
                Stats = mobStatCollection.GetByLevel(level),
                ActiveSkill = _skillBluePrintFactory.Create(weaponData, level, id)
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
    }
}