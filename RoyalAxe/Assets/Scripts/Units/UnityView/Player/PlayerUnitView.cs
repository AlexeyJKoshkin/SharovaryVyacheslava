using UnityEngine;

namespace RoyalAxe.Units.Player
{
    public class PlayerUnitView : UnitsView
    {
        public Transform SkillSpawnTransform => _playerSkillSpawnPosition;
        [SerializeField] private Transform _playerSkillSpawnPosition;
    }
}