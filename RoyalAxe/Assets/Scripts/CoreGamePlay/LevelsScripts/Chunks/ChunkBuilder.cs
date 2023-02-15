using System.Collections.Generic;
using System.Linq;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CoreLevel
{
    public class ChunkBuilderHelper
    {
        private readonly CoreGamePlayContext _gamePlayContext;
        private readonly Queue<LevelChunkView> _queue = new Queue<LevelChunkView>();

        public ChunkBuilderHelper(CoreGamePlayContext gamePlayContext)
        {
            _gamePlayContext = gamePlayContext;
       }

        public CoreGamePlayEntity CreateChunk()
        {
            var chunkEntity = _gamePlayContext.CreateEntity();
            var view        = Get();
            var chunkBounds = view.CalcChunkBounds();
            chunkEntity.AddChunkView(view);
            chunkEntity.ReplaceChunkBounds(chunkBounds);
            return chunkEntity;
        }

        public void Destroy(CoreGamePlayEntity chunk)
        {
            Return(chunk.chunkView.View);
            chunk.Destroy();
        }


        public void Initialize(BiomeScriptableDef scriptableDef)
        {
            var instanTiatedhunks = scriptableDef.Chunks.Select(Object.Instantiate);
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
           //todo: подумать над уничтожением всех элементов на чанке
            chunk.SetActive(false);
            _queue.Enqueue(chunk);
        }
    }
}