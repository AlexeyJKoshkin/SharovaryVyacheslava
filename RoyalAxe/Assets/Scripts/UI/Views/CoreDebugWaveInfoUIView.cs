using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelBuff;
using RoyalAxe.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Serializable]
    public class PlayerBuffControlPanel
    {
        public GameObject Prefab;
        public Transform Root;

        
        private List<PlayerLevelPowerViewModel> _viewModels = new List<PlayerLevelPowerViewModel>();

        public void Init(ILevelBuffStorage storage)
        {
            foreach (var type in storage.ExistsBuffs)
            {
                var go = GameObject.Instantiate(Prefab, Root);
                go.SetActive(true);
                var buff = new PlayerLevelPowerViewModel(go, storage.Peek(type));
                _viewModels.Add(buff);
            }
        }

        class PlayerLevelPowerViewModel
        {
            public ILevelPowerStrategy LevelPowerStrategy;

            private TextMeshProUGUI _text;
            public Image Image;

            public PlayerLevelPowerViewModel(GameObject gameObject, ILevelPowerStrategy levelPowerStrategy)
            {
                LevelPowerStrategy = levelPowerStrategy;
                var _button = gameObject.GetComponentInChildren<Button>();
                _button.onClick.AddListener(OnClickHandler);
                _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                Image = gameObject.GetComponentInChildren<Image>();
                UpdateView();
            }

            private void UpdateView()
            {
                var t = LevelPowerStrategy.IsSingle ? LevelPowerStrategy.IsActive ?"Active" :"Not"  : "inf";
                _text.text = $"{LevelPowerStrategy.Type} {t}";
            }

            private void OnClickHandler()
            {
                if(LevelPowerStrategy.IsActive)
                    LevelPowerStrategy.DeActivate();
                else LevelPowerStrategy.Activate();
                
                UpdateView();
            }
        }
    }
}