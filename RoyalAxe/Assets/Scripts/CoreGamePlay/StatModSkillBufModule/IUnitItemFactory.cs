using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats 
{
    public interface IUnitItemFactory
    {
        MainWeaponItem CreateMainItem(WeaponBluePrint weaponBluePrint);
    }
}