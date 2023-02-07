using Core.Data.Provider;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.CoreLevel 
{
    public class ItemsBluePrintsFactory : IItemsBlueprintsFactory
    {
        private readonly IDataStorage _dataStorage;
        private readonly ISkillBlueprintsFactory _skillBlueprints;
        public ItemsBluePrintsFactory(IDataStorage dataStorage, ISkillBlueprintsFactory skillBlueprints)
        {
            _dataStorage     = dataStorage;
            _skillBlueprints = skillBlueprints;
        }

        public WeaponBluePrint CreateHeroMainWeapon(string id, int level)
        {
            var weapon = _dataStorage.ById<UnitWeaponSkillConfigDef>(id);
            return CreateMainWeapon(weapon,id, level);
        }

        public WeaponBluePrint CreateMainWeapon(SkillConfigDef weapon, string id, int level)
        {
            WeaponBluePrint result = new WeaponBluePrint()
            {
                Id             = id,Level = level,
                SkillBlueprint = _skillBlueprints.Create(weapon,id,level)
            };
            return result;
        }
    }
}