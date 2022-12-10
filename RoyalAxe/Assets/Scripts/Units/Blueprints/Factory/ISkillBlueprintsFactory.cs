using RoyalAxe.Units.Stats;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel {
    public interface ISkillBlueprintsFactory
    {
        SkillBlueprint Create(WeaponsSkillConfigDef weaponData, int level);
    }

    public interface IItemsBlueprintsFactory
    {
        WeaponBluePrint CreateMainWeapon(string weaponRecordId, int weaponRecordLevel);
        WeaponBluePrint CreateMainWeapon(WeaponsSkillConfigDef weaponRecordId, int weaponRecordLevel);
    }
}