using System.Collections.Generic;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    public interface ILevelAdapter : ICoreLevelBuilder, IBound
    {
        Transform ChunkRoot { get; }
      //  BiomeScriptableDef BiomeDef { get; }
        IReadOnlyList<EndPointMeleeMobPoint> EndPointsModels { get; }
        void HandleNextChunk(CoreGamePlayEntity chunk);
    }

    public class LevelAdapter : ILevelAdapter
    {
        public Transform ChunkRoot => _view.ChunkRoot;
        public Bounds Bounds => _view.Bounds;
        public IReadOnlyList<EndPointMeleeMobPoint> EndPointsModels => _view.MeleeMobEndPoints;

        private readonly IChunkPositionCalculation _chunkPositionCalculation;

        private readonly LevelInfrastructureView _view;
        private readonly CoreGamePlayContext _coreGamePlayContext;
        private readonly ChunkBuilderHelper _chunkBuilderHelper;
        private CoreGamePlayEntity BearingSpawnChunk => _coreGamePlayContext.bearingSpawnChunkEntity;
        
        public LevelAdapter(LevelInfrastructureView view, CoreGamePlayContext coreGamePlayContext, IChunkPositionCalculation chunkPositionCalculation)
        {
            _view = view;
            _coreGamePlayContext = coreGamePlayContext;
            _chunkPositionCalculation = chunkPositionCalculation;
            _chunkBuilderHelper = new ChunkBuilderHelper(coreGamePlayContext, this, chunkPositionCalculation);
        }


        public void BuildLevel(ICoreLevelDataInfrastructure levelDataInfrastructure)
        {
            _chunkBuilderHelper.Initialize(levelDataInfrastructure.BiomeDef);
            
            var startChunk = _chunkBuilderHelper.CreateChunk();
            SetChunkToStartPoint(startChunk); // Установили первый чанк
            var nextChunk = _chunkBuilderHelper.CreateChunk(); 
            SetNextChunk(nextChunk);
        }
        
        void ILevelAdapter.HandleNextChunk(CoreGamePlayEntity chunk)
        {
            var nextChunk = _chunkBuilderHelper.CreateChunk();
            SetNextChunk(nextChunk);
            _chunkBuilderHelper.Destroy(chunk);
        }
        
        private void SetNextChunk(CoreGamePlayEntity nextChunk)
        {
         
            var nextChunkPos = _chunkPositionCalculation.CalcNextChunkPos( BearingSpawnChunk.chunkBounds, nextChunk.chunkBounds);
            SetChunkPos(nextChunk, nextChunkPos);
            BearingSpawnChunk.isBearingSpawnChunk = false;
            SetNewBearingChunk(nextChunk);
        }

        private void SetChunkToStartPoint(CoreGamePlayEntity startChunk) // установили чанк на стартовую позицию
        {
            var startChunkPos = _chunkPositionCalculation.CalcStartChunkPos(startChunk.chunkBounds);
            
            SetChunkPos(startChunk,startChunkPos);
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