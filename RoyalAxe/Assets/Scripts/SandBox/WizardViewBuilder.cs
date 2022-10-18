using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    public interface IWizardViewBuilder
    {
        WizardTrigger CreateWizard();
    }
    
    public class WizardViewBuilder : IWizardViewBuilder
    {
        private readonly CoreGamePlayPrefabStorage _prefabStorage;
        private readonly IChunkPositionCalculation _chunkPositionCalculation;
        private readonly ILevelAdapter _levelAdapter;

        public WizardViewBuilder(CoreGamePlayPrefabStorage prefabStorage, IChunkPositionCalculation chunkPositionCalculation, ILevelAdapter levelAdapter)
        {
            _prefabStorage            = prefabStorage;
            _chunkPositionCalculation = chunkPositionCalculation;
            _levelAdapter             = levelAdapter;
        }

        public WizardTrigger CreateWizard()
        {
            var prefab = _prefabStorage.WizardTrigger;
            var result = Object.Instantiate(prefab, _levelAdapter.ChunkRoot);
            result.transform.position = _chunkPositionCalculation.CalcWizardPosition();
            return result;
        }
    }
}