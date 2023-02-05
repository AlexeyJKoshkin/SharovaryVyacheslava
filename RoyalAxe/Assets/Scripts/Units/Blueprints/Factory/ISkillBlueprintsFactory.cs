using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.CoreLevel {
    public interface ISkillBlueprintsFactory
    {
        SkillBlueprint Create(SkillConfigDef weaponData, int level);
    }

    public interface IItemsBlueprintsFactory
    {
        WeaponBluePrint CreateMainWeapon(string weaponRecordId, int weaponRecordLevel);
        WeaponBluePrint CreateMainWeapon(SkillConfigDef weaponRecordId, int weaponRecordLevel);
    }
}