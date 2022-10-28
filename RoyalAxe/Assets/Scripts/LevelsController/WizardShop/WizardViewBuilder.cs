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
        private readonly ILevelPositionCalculation _levelPositionCalculation;
        private readonly ILevelAdapter _levelAdapter;

        public WizardViewBuilder(CoreGamePlayPrefabStorage prefabStorage, ILevelPositionCalculation levelPositionCalculation, ILevelAdapter levelAdapter)
        {
            _prefabStorage            = prefabStorage;
            _levelPositionCalculation = levelPositionCalculation;
            _levelAdapter             = levelAdapter;
        }

        public WizardTrigger CreateWizard()
        {
            var prefab = _prefabStorage.WizardTrigger;
            var result = Object.Instantiate(prefab, _levelAdapter.ChunkRoot);
            result.transform.position = _levelPositionCalculation.CalcWizardPosition(result);
            return result;
        }
    }
}