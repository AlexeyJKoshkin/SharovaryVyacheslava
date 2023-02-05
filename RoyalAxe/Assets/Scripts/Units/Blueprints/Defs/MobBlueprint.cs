using UnityEngine;

namespace RoyalAxe.GameEntitas {
    public class MobBlueprint : UnitBlueprint
    {
        public Vector2 Position;

        public MobBlueprint(string id, int level) : base(id, level)
        {
        }
    }
}