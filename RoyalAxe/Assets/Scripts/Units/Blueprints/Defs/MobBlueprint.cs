using RoyalAxe.CoreLevel;
using UnityEngine;

namespace RoyalAxe.GameEntitas {
    public class MobBlueprint : UnitBlueprint
    {
        public Vector2 Position;

        public MobDeathReward DeathReward;

        public MobBlueprint(string id, int level) : base(id, level)
        {
        }
    }
}