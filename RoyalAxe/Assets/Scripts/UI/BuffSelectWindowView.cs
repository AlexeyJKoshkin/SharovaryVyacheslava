using System;
using System.Linq;
using GameKit;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalAxe 
{
    public class BuffSelectWindowView : MonoBehaviour
    {
        [SerializeField]
        private BuffBntView[] _bntViews;

        private void OnDisable()
        {
            _bntViews.ForEach(e=> e.Reset());
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
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
            
            [SerializeField]
            private TextMeshProUGUI _text;
            
            [SerializeField]
            private Button _button;

            public BuffBntView()
            {
                
            }

            public BuffBntView(Button button)
            {
                _button = button;
                _text =  button.GetComponentInChildren<TextMeshProUGUI>();
            }

            public void Reset()
            {
                _button.gameObject.SetActive(false);
                _text.text = "";
                _button.onClick.RemoveAllListeners();
            }
        }
    }

    
}