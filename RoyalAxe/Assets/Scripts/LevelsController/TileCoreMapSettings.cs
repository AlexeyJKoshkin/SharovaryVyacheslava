using UnityEngine;

namespace RoyalAxe.Map
{
    public class TileCoreMapSettings : ScriptableObject
    {
        public float CellSize = 1f;
        public float PPU = 500;

        public Vector2Int AreaSize;
        public float ChunkSpeed => _chunkSpeed;
        [SerializeField] private float _chunkSpeed = 1;
    }
}