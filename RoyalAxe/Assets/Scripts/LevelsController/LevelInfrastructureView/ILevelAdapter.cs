using System.Collections.Generic;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    public interface ILevelAdapter : ICoreLevelBuilder
    {
        Transform ChunkRoot { get; }
        Bounds Bounds { get; }
        BiomeScriptableDef BiomeDef { get; }
        IReadOnlyList<EndPointMeleeMobPoint> EndPointsModels { get; }

        void HandleNextChunk(CoreGamePlayEntity chunk);
    }

    public class LevelAdapter : ILevelAdapter
    {
        public Transform ChunkRoot => _view.ChunkRoot;
        public Bounds Bounds => _view.Bounds;
        public BiomeScriptableDef BiomeDef => _view.BiomeDef;
        public IReadOnlyList<EndPointMeleeMobPoint> EndPointsModels => _view.MeleeMobEndPoints;

        private LevelInfrastructureView _view;
        private readonly CoreGamePlayContext _coreGamePlayContext;
        private readonly ChunkBuilderHelper _chunkBuilderHelper;
        private CoreGamePlayEntity BearingSpawnChunk => _coreGamePlayContext.bearingSpawnChunkEntity;
        
        public LevelAdapter(LevelInfrastructureView view, CoreGamePlayContext coreGamePlayContext)
        {
            _view = view;
            _coreGamePlayContext = coreGamePlayContext;
            _chunkBuilderHelper = new ChunkBuilderHelper(coreGamePlayContext, this);
        }


        public void BuildLevel(ICoreLevelDataInfrastructure levelNumber)
        {
            var startChunk = _chunkBuilderHelper.CreateChunk();
            SetChunkToStartPoint(startChunk); // Установили первый чанк
            var nextChunk = _chunkBuilderHelper.CreateChunk(); 
            SetNextChunk(nextChunk);
            _view.TurnOffBorders();
        }
        
        void ILevelAdapter.HandleNextChunk(CoreGamePlayEntity chunk)
        {
            var nextChunk = _chunkBuilderHelper.CreateChunk();
            SetNextChunk(nextChunk);
            _chunkBuilderHelper.Destroy(chunk);
        }
        
        private void SetNextChunk(CoreGamePlayEntity nextChunk)
        {
            var upPosition = BearingSpawnChunk.chunkBounds.Max.y;
            var halfSize   = nextChunk.chunkBounds.Extents.y;
            upPosition += halfSize;
            SetChunkPos(nextChunk, upPosition);
            BearingSpawnChunk.isBearingSpawnChunk = false;
            SetNewBearingChunk(nextChunk);
        }

        private void SetChunkToStartPoint(CoreGamePlayEntity startChunk) // установили чанк на стартовую позицию
        {
            var downPosition = _view.Bounds.min.y;
            var halfSize     = startChunk.chunkBounds.Extents.y;
            SetChunkPos(startChunk, downPosition + halfSize);
            SetNewBearingChunk(startChunk);
        }

        void SetNewBearingChunk(CoreGamePlayEntity nextChunk)
        {
            nextChunk.isBearingSpawnChunk = true;
        }

        void SetChunkPos(CoreGamePlayEntity chunk, float yPos)
        {
            var currentPos = _view.ChunkRoot.position;
            var view       = chunk.chunkView.View;
            currentPos.y                = yPos;
            view.RootTransform.position = currentPos;
            chunk.ReplaceChunkBounds(view.CalcChunkBounds());
        }
    }
}