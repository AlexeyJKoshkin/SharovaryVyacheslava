// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;
using UnityEngine.Events;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Toggles a gameObject active state. Intended as an optional class to help toggle windows on and off in some use cases.
    /// Depending on your project requirements you may or may not need this script.
    /// </summary>
    public class UiPanelVisibility : MonoBehaviour
    {
        public bool StartClosed;
        public UnityEvent OnShow;
        public UnityEvent OnHide;

        protected virtual void Start()
        {
            if (StartClosed)
            {
                Hide();
            }
        }
        public void Show()
        {
            gameObject.SetActive(false);
            OnShow?.Invoke();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }
}