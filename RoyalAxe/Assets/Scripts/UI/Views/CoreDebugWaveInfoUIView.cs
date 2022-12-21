using System.Collections.Generic;
using System.Linq;
using Entitas;
using RoyalAxe.CoreLevel;
using RoyalAxe.Units;
using TMPro;
using UnityEngine;

namespace RoyalAxe 
{
    public class CoreDebugWaveInfoUIView : UIViewComponent,ILevelNumberListener,ILevelMobBluePrintsListener,IViewEntityBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _MobLevelInfoText;

        [SerializeField]
        private PlayerBuffControlPanel _levelPowerPanel;
        public PlayerBuffControlPanel PlayerLevelPowerView => _levelPowerPanel;

        public void InitEntity(IEntity entity)
        {
            if (entity is CoreGamePlayEntity coreGamePlayEntity)
            {
                if (!coreGamePlayEntity.isLevelWave) return;
                coreGamePlayEntity.AddLevelNumberListener(this);
                coreGamePlayEntity.AddLevelMobBluePrintsListener(this);
            }
        }
        
        public void OnLevelNumber(CoreGamePlayEntity entity, int number)
        {
        }

        public void OnLevelMobBluePrints(CoreGamePlayEntity entity, List<GenerateMobBlueprintCounter> collection)
        {
            var total = collection.Count == 0 ? 0 : collection.Sum(o => o.TotalAmount);
            var max   = entity.levelWaveQueue.Current.MaxMobAmount;
            _MobLevelInfoText.text = $"Left: {total}/ Max Spawn Amount {max}";
        }
    }
}