using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectUI
{
    public class MetaSceneUIView : MonoBehaviour, IMetaSceneUIViewHolder
    {
        public TempMenu TempView => _tempMenu;

        [SerializeField]
        private TempMenu _tempMenu = new TempMenu();
        
        public void InitMainSceneState()
        {
           
        }

        private void Awake()
        {
            _tempMenu.Init();
        }

        [Serializable]
        public class TempMenu
        {
            public event Action OnClickStartGameBtn;
            
            [SerializeField]
            private Button _startButton;

            public void Init()
            {
                _startButton.onClick.AddListener(InvokeClick);
            }

            private void InvokeClick()
            {
                OnClickStartGameBtn?.Invoke();
            }

       
        }
    }
    
}
