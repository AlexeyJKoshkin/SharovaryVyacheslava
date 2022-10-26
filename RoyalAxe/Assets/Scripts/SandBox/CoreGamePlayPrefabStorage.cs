using Entitas;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    [CreateAssetMenu]
    public class CoreGamePlayPrefabStorage : ScriptableObject
    {
        public WizardTrigger WizardTrigger;
    }

    public interface ICoreGameUtility
    {
        void ClearAllBeforeLeaveCoreScene();
    }

    public class CoreGameUtility : ICoreGameUtility
    {
        private readonly Contexts _contexts;
        public CoreGameUtility(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void ClearAllBeforeLeaveCoreScene()
        {
            Debug.LogError("On Exit Core State");
            ClearContext(_contexts.units);
            ClearContext(_contexts.skill);
            ClearContext(_contexts.rAAnimation);
            ClearContext(_contexts.coreGamePlay);
        }

        private void ClearContext(IContext contextUnits)
        {
            contextUnits.ClearComponentPools();
            contextUnits.Reset();
        }

       
    }
}