using System.Collections.Generic;
using UnityEngine;

namespace RoyalAxe.CoreLevel {
    public class EndPointsRoyalAxeMap
    {
        public Vector2 Position { get; private set; }

        private HashSet<string> _mobIds;
        public EndPointsRoyalAxeMap(EndPointMeleeMobPoint model)
        {
            _mobIds  = new HashSet<string>(model.MobIds);
            Position = model.PointPosition;
        }

        public bool Contains(string modDataMobId)
        {
            return _mobIds.Count == 0 || _mobIds.Contains(modDataMobId);
        }
    }
}