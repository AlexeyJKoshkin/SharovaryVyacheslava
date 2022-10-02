using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.CoreLevel;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    [CoreGamePlay]
    public class ChunkViewComponent : BaseViewComponent<LevelChunkView>
    {
        public LevelChunkViewModel ViewModel => View.ViewModel;
        public Transform RootTransform => View.RootTransform;
    }

    [CoreGamePlay]
    public class MovingChunkComponent : IComponent
    {
        public float YEndPoint;
    }

    [CoreGamePlay]
    [Unique]
    public class BearingSpawnChunk : IComponent { }

    [CoreGamePlay]
    public class ChunkBoundsComponent : IComponent
    {
        public Bounds Bounds;
        public Vector3 Extents => Bounds.extents;
        public Vector3 Max => Bounds.max;

        public bool Contains(Vector3 position)
        {
            return Bounds.Contains(position);
        }
    }
}
