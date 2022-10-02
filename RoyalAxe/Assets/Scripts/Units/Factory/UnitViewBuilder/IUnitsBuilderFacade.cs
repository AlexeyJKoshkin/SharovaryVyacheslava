using Core.UserProfile;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public interface IUnitsBuilderFacade
    {
        UnitsEntity CreateEnemyMobUnit(string testMobUniqueId, byte level, Vector2 pos);

        void CreatePlayer(HeroProgressData selectedHero, WeaponProgressData selectedWeapon);
    }
}