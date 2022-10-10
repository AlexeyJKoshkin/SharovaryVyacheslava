using System.Collections.Generic;
using System.Linq;
using Entitas;
using GameKit;
using UnityEngine;
using VContainer.Unity;

namespace RoyalAxe.CoreLevel
{
    public class ChunkBuilderHelper
    {
        private readonly CoreGamePlayContext _gamePlayContext;
        readonly ILevelAdapter _levelView;
        private Bounds _levelViewBounds;
        private readonly Queue<LevelChunkView> _queue = new Queue<LevelChunkView>();
        private IChunkPositionCalculation _chunkPositionCalculation;

        public ChunkBuilderHelper(CoreGamePlayContext gamePlayContext, ILevelAdapter levelView, IChunkPositionCalculation chunkPositionCalculation)
        {
            _gamePlayContext = gamePlayContext;
            _levelView       = levelView;
            _chunkPositionCalculation = chunkPositionCalculation;
            _levelViewBounds = levelView.Bounds;
            Initialize();
        }

        public CoreGamePlayEntity CreateChunk()
        {
            var chunkEntity = _gamePlayContext.CreateEntity();
            var view        = Get();
            var chunkBounds = view.CalcChunkBounds();
            chunkEntity.AddChunkView(view);
            chunkEntity.ReplaceChunkBounds(chunkBounds);
            var endPoint = _chunkPositionCalculation.GetEndPoint(_levelViewBounds, chunkBounds);
            chunkEntity.AddMovingChunk(endPoint);
            return chunkEntity;
        }

        public void Destroy(CoreGamePlayEntity chunk)
        {
            Return(chunk.chunkView.View);
            chunk.Destroy();
        }


        public void Initialize()
        {
            var instanTiatedhunks = _levelView.BiomeDef.Chunks.Select(e => Object.Instantiate(e, _levelView.ChunkRoot));
            _queue.Clear();
            instanTiatedhunks.ForEach(e =>
                                      {
                                          e.SetActive(false);
                                          _queue.Enqueue(e);
                                      });
        }
        
        LevelChunkView Get()
        {
            if (_queue.Count == 0) return null;
            var result = _queue.Dequeue();
            result.SetActive(true);
            return result;
        }

        void Return(LevelChunkView chunk)
        {
            //подумать над уничтожением всех элементов на чанке
            chunk.SetActive(false);
            _queue.Enqueue(chunk);
        }
    }
}