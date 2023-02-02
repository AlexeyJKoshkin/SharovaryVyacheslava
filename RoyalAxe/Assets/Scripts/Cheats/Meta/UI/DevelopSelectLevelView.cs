using Core.UserProfile;
using RoyalAxe.CoreLevel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoyalAxe
{
    public class DevelopSelectLevelView : MonoBehaviour
    {
        public event UnityAction<int> OnChangeLevelEvent;


        [SerializeField] TMP_InputField _input;

        [SerializeField] TextMeshProUGUI _levelInfo;


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
                _levelInfo.text = "Уровень из сохранения";
             //   _input.text     = "Enter level";
            }
            else
            {
                _levelInfo.text = $"Biome {levelSettingsData.Type} MaxMobAmount {levelSettingsData.MaxMobAmount} SpawnCooldown {levelSettingsData.SpawnCooldown}\nDestiny {levelSettingsData.Destiny}";
                _input.text     = levelSettingsData.LevelNumber.ToString();
            }
        }

        private void OnChangeInputValue(string text)
        {
            var inputIntValue = string.IsNullOrEmpty(text) ? -1 : int.Parse(text);
            OnChangeLevelEvent?.Invoke(inputIntValue);
        }
    }
}