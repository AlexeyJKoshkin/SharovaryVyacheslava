using Core.Data.Provider;
using GameKit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RoyalAxe.CoreLevel
{
    public class LevelChunkView : MonoBehaviour, IDataObject
    {
     
        public Transform RootTransform => _rootTransform;
        [SerializeField,GameKit.ReadOnly]
        private Transform _rootTransform;
        string IDataObject.UniqueID => BiomeType.ToString();
        public LevelChunkViewModel ViewModel;
        public BiomeType BiomeType => ViewModel.BiomeType;
        [field: SerializeField] public Tilemap TileMap { get; private set; }

        [HideIf("TileMap", null)]
        [Button]
        void CompressBounds()
        {
            TileMap.CompressBounds();
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        
        public Bounds CalcChunkBounds()
        {
            var cellSize   = TileMap.cellSize;
            var cellBounds = TileMap.cellBounds;
            return new Bounds(RootTransform.position, new Vector2(cellSize.x*cellBounds.size.x, cellSize.y*cellBounds.size.y));
        }

        private void OnDrawGizmos()
        {
            if (TileMap == null) return;
            var cellSize = TileMap.cellSize;

            var bounds = CalcChunkBounds();
            Gizmos.color = TileMap.color;
            Gizmos.DrawLine(bounds.min, new Vector2(bounds.max.x, bounds.min.y));
            Gizmos.DrawLine(new Vector2(bounds.max.x, bounds.max.y), new Vector2(bounds.min.x, bounds.max.y));
            Gizmos.DrawLine(new Vector2(bounds.min.x, bounds.max.y), bounds.min);

            Vector2 startposition = bounds.min;
            Vector2 endPosition   = startposition + new Vector2(0, TileMap.size.y * cellSize.y);

            void DrawGizmoLine(LineModel obj)
            {
                var delta = obj.Size * cellSize.x;
                startposition.x += delta;
                endPosition.x   += delta;
                Gizmos.DrawLine(startposition, endPosition);
            }
        }

        private void Reset()
        {
            _rootTransform = this.transform;
        }
    }
}