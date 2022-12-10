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

        public WeaponBluePrint CreateMainWeapon(string id, int level)
        {
            var weapon = _dataStorage.ById<WeaponsSkillConfigDef>(id);
            return CreateMainWeapon(weapon, level);

        }

        public WeaponBluePrint CreateMainWeapon(WeaponsSkillConfigDef weapon, int level)
        {
            WeaponBluePrint result = new WeaponBluePrint()
            {
                Id             = weapon.UniqueID,Level = level,
                SkillBlueprint = _skillBlueprints.Create(weapon,level)
            };
            return result;
        }
    }
}