using System;
using System.Collections.Generic;
using RoyalAxe.Map;
using RoyalAxe.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class LevelInfrastructureView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform ChunkRoot { get; private set; }

        public IReadOnlyList<EndPointMeleeMobPoint> MeleeMobEndPoints => _meleeMobEndPoints;
        
        [SerializeField]
        private List<EndPointMeleeMobPoint> _meleeMobEndPoints;
        
        [field: SerializeField]
        public Transform PlayerStartPoint { get; private set; }

        public Bounds Bounds => _borderCollider.bounds;
        public TileCoreMapSettings TimeMapCoreSettings;
        public BiomeScriptableDef BiomeDef;

        [SerializeField]
        private BoxCollider2D _borderCollider;
    }


    [Serializable]
    public class EndPointMeleeMobPoint
    {
        [SerializeField, MobId]
        public string[] MobIds;
        public Vector2 PointPosition => _endPoint.position;
        [SerializeField]
        private Transform _endPoint;
    }
}