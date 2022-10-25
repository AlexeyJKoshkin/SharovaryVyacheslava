using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameKit;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoyalAxe
{
    public class BuffSelectWindowView : UIViewComponent
    {
        [SerializeField] private BuffBntView[] _bntViews;
        public IReadOnlyList<BuffBntView> BuffBtns => _bntViews;

        private void OnDisable()
        {
            _bntViews.ForEach(e => e.Reset());
        }

        [Button]
        void FindBtn()
        {
            var layot = GetComponentInChildren<LayoutGroup>();
            _bntViews = layot.GetComponentsInChildren<Button>().Select(o => new BuffBntView(o)).ToArray();
        }

        [Serializable]
        public class BuffBntView
        {
            public string Text
            {
                set => _text.text = value;
            }

            [SerializeField] private TextMeshProUGUI _text;

            [SerializeField] private Button _button;

            public BuffBntView() { }

            public BuffBntView(Button button)
            {
                _button = button;
                _text   = button.GetComponentInChildren<TextMeshProUGUI>();
            }

            public void Reset()
            {
                _button.gameObject.SetActive(false);
                _text.text = "";
                _button.onClick.RemoveAllListeners();
            }

            public void AddCallback(UnityAction action)
            {
                _button.onClick.AddListener(action);
            }

            public void TurnOn()
            {
                _button.gameObject.SetActive(true);
            }
        }
    }
}