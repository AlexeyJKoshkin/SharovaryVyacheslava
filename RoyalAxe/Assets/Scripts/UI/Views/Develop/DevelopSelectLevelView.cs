using System;
using RoyalAxe.CoreLevel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoyalAxe 
{
    [Serializable]
    public class DevelopSelectLevelView : MonoBehaviour
    {
        public event UnityAction<int> OnChangeLevelEvent;

        public int InputIntValue
        {
            get { return string.IsNullOrEmpty(_input.text) ? -1 : int.Parse(_input.text); }
            set { _input.text = value.ToString(); }
        }
        [SerializeField]
        TMP_InputField _input;
        
        [SerializeField]
        TextMeshProUGUI _levelInfo;

        private int _previous;
        
        private void OnEnable()
        {
            _input.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _input.onValueChanged.AddListener(OnChangeInputValue);
        }

        private void OnDisable()
        {
            _input.onValueChanged.RemoveListener(OnChangeInputValue);
        }

        public void DrawLevel(LevelSettingsData levelSettingsData)
        {
            if (levelSettingsData == null)
            {
                _levelInfo.text = "From Save";
                _previous        = -1;
            }
            else
            {
                _levelInfo.text = $"Biome {levelSettingsData.Type} MaxMobAmount {levelSettingsData.MaxMobAmount} SpawnCooldown {levelSettingsData.SpawnCooldown}\nDestiny {levelSettingsData.Destiny}";
                _previous        = levelSettingsData.LevelNumber;
            }
        }

        private void OnChangeInputValue(string text)
        {
            var selected = InputIntValue;
            if(selected!= _previous)
                OnChangeLevelEvent?.Invoke(selected);
        }
    }
}