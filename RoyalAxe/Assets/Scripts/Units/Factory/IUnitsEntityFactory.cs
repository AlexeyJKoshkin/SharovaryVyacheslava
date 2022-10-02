using Core.UserProfile;

namespace RoyalAxe.GameEntitas
{
    public interface IUnitsEntityFactory
    {
        UnitsEntity CreateEnemyMobUnit(string id, byte level = 1);

        UnitsEntity CreateEnemyMobBoson(UnitsEntity owner);
        UnitsEntity CreatePlayerBoson(UnitsEntity owner);
        UnitsEntity CreatePlayer(HeroProgressData characterConfigUniqueId, WeaponProgressData selectedWeapon);
    }
}