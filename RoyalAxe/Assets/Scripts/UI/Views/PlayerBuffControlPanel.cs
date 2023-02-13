using System;
using System.Collections.Generic;
using RoyalAxe.LevelSkill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace RoyalAxe 
{
    [Serializable]
    public class PlayerBuffControlPanel
    {
        public GameObject Prefab;
        public Transform Root;

        
        private List<PlayerLevelPowerViewModel> _viewModels = new List<PlayerLevelPowerViewModel>();

        public void Init(ILevelSkillStorage storage)
        {
            foreach (var skill in storage)
            {
                var go = Object.Instantiate(Prefab, Root);
                go.SetActive(true);
                var buff = new PlayerLevelPowerViewModel(go, skill);
                _viewModels.Add(buff);
            }
        }

        class PlayerLevelPowerViewModel
        {
            public ILevelSkill LevelSkill;

            private TextMeshProUGUI _text;
            public Image Image;

            public PlayerLevelPowerViewModel(GameObject gameObject, ILevelSkill levelSkill)
            {
                LevelSkill = levelSkill;
                var _button = gameObject.GetComponentInChildren<Button>();
                _button.onClick.AddListener(OnClickHandler);
                _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                Image = gameObject.GetComponentInChildren<Image>();
                UpdateView();
            }

            private void UpdateView()
            {
                var t = LevelSkill.IsSingle ? LevelSkill.IsActive ?"Active" :"Not"  : "inf";
                _text.text = $"{LevelSkill.Type} {t}";
            }

            private void OnClickHandler()
            {
                if(LevelSkill.IsActive)
                    LevelSkill.DeActivate();
                else LevelSkill.Activate();
                
                UpdateView();
            }
        }
    }

    [Serializable]
    public class UnitDamageInfoPanel
    {
        [SerializeField]
        public TextMeshProUGUI _singleDamageInfo;




    }
}