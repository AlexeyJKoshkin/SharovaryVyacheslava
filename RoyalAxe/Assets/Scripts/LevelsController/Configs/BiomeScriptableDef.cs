using System.Collections.Generic;
using Core.Data.Provider;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    //todo: надо разбить биомы на бандлы. 
    public class BiomeScriptableDef : ScriptableObject, IDataObject
    {
        public IReadOnlyCollection<LevelChunkView> Chunks => _chunks;
        
        [SerializeField]
        BiomeType _type;
        [SerializeField] 
        private List<LevelChunkView> _chunks;
        public string UniqueID => name;
        public LineModel[] Lines;
    }
}