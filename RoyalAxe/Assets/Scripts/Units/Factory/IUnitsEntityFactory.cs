using Core.UserProfile;

namespace RoyalAxe.GameEntitas
{
    public interface IUnitsEntityFactory
    {
        UnitsEntity CreateEnemyMobUnit(MobBlueprint mobBlueprint);

        UnitsEntity CreateEnemyMobBoson(UnitsEntity owner);
        UnitsEntity CreatePlayerBoson(UnitsEntity owner);
        //UnitsEntity CreatePlayer(HeroProgressData characterConfigUniqueId, WeaponProgressData selectedWeapon);
        UnitsEntity CreatePlayer(UnitBlueprint unitBlueprint);
        UnitsEntity CreateWizardUnit();
    }
}