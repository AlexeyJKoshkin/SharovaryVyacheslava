using UnityEngine;

namespace RoyalAxe.Units.Mob
{
    public class GunnerMobView : UnitsView
    {
        public Transform CannonBallSpawn => _cannonBallSpawnPosition;
        [SerializeField] private Transform _cannonBallSpawnPosition;
    }
}