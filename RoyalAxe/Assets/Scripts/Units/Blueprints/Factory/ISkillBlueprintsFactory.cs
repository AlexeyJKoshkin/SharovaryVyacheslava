using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.CoreLevel {
    public interface ISkillBlueprintsFactory
    {
        SkillBlueprint Create(SkillConfigDef weaponData,  string id, int level);
    }

    public interface IItemsBlueprintsFactory
    {
        WeaponBluePrint CreateHeroMainWeapon(string weaponRecordId, int weaponRecordLevel);
        WeaponBluePrint CreateMainWeapon(SkillConfigDef weaponRecordId, string id, int weaponRecordLevel);
    }
}