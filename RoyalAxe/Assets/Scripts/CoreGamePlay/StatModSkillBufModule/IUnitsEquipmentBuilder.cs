using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats {
    public interface IUnitsEquipmentBuilder
    {
        void EquipMobWeapon(UnitsEntity unit, WeaponBluePrint weaponBluePrint);
        void EquipPlayer(UnitsEntity player, WeaponBluePrint mainWeapon);
    }
}