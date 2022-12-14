using System.Collections.Generic;
using System.Linq;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkBuilderHelper
    {
        private readonly CoreGamePlayContext _gamePlayContext;
        readonly ILevelAdapter _levelView;
        private readonly Bounds _levelViewBounds;
        private readonly Queue<LevelChunkView> _queue = new Queue<LevelChunkView>();
        private readonly ILevelPositionCalculation _levelPositionCalculation;

        public ChunkBuilderHelper(CoreGamePlayContext gamePlayContext, ILevelAdapter levelView, ILevelPositionCalculation levelPositionCalculation)
        {
            _gamePlayContext = gamePlayContext;
            _levelView       = levelView;
            _levelPositionCalculation = levelPositionCalculation;
            _levelViewBounds = levelView.Bounds;
       }

        public CoreGamePlayEntity CreateChunk()
        {
            var chunkEntity = _gamePlayContext.CreateEntity();
            var view        = Get();
            var chunkBounds = view.CalcChunkBounds();
            chunkEntity.AddChunkView(view);
            chunkEntity.ReplaceChunkBounds(chunkBounds);
            var endPoint = _levelPositionCalculation.GetEndPoint(_levelViewBounds, chunkBounds);
            chunkEntity.AddMovingChunk(endPoint);
            return chunkEntity;
        }

        public void Destroy(CoreGamePlayEntity chunk)
        {
            Return(chunk.chunkView.View);
            chunk.Destroy();
        }


        public void Initialize(BiomeScriptableDef scriptableDef)
        {
            var instanTiatedhunks = scriptableDef.Chunks.Select(e => Object.Instantiate(e, _levelView.ChunkRoot));
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
            //???????????????? ?????? ???????????????????????? ???????? ?????????????????? ???? ??????????
            chunk.SetActive(false);
            _queue.Enqueue(chunk);
        }
    }
}