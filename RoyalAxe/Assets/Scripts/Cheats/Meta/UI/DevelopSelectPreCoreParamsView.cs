using System;
using System.Collections.Generic;
using Core.UserProfile;
using TMPro;
using UnityEngine;

namespace RoyalAxe
{
    public class DevelopSelectPreCoreParamsView : MonoBehaviour
    {
        public event Action<string, int> OnChangeSelectionItemParamsEvent;
        public int Level => _itemSelector.value > 0 ? string.IsNullOrEmpty(_levelInput.text) ? 1 : int.Parse(_levelInput.text) : 0;
        public int ItemIndex => _itemSelector.value;

        [SerializeField] TMP_InputField _levelInput;
        [SerializeField] TMP_Dropdown _itemSelector;


        public void SetItemData(List<string> ids, SaveEntityRecord progressData)
        {
            _itemSelector.ClearOptions();
            _itemSelector.AddOptions(ids);

            if (progressData == null) SetItem(0, 0);
            else
            {
                var index = ids.FindIndex(o => o == progressData.Id);
                index = Mathf.Max(index, 0);
                SetItem(index, progressData.Level);
            }
        }


        void SetItem(int heroIndex, int heroLevel)
        {
            _itemSelector.value = heroIndex;

            _levelInput.text         = heroLevel.ToString();
            _levelInput.interactable = heroIndex > 0;
        }


        private void OnChangeInputValue(string text)
        {
            var optionData = _itemSelector.options[_itemSelector.value];
            OnChangeSelectionItemParamsEvent?.Invoke(optionData.text, Level);
        }

        private void OnChangeItemIndex(int index)
        {
            _levelInput.interactable = index > 0;
            _levelInput.text = Mathf.Max(1, Level).ToString();
            var optionData = _itemSelector.options[index];
            OnChangeSelectionItemParamsEvent?.Invoke(optionData.text, Level);
        }

        private void OnEnable()
        {
            _levelInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _levelInput.onValueChanged.AddListener(OnChangeInputValue);
            _itemSelector.onValueChanged.AddListener(OnChangeItemIndex);
        }

        private void OnDisable()
        {
            _levelInput.onValueChanged.RemoveListener(OnChangeInputValue);
        }

        private void Reset()
        {
            this._itemSelector = GetComponentInChildren<TMP_Dropdown>();
            this._levelInput   = GetComponentInChildren<TMP_InputField>();
        }
    }
}