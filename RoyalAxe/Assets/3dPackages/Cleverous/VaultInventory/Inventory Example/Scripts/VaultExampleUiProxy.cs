// (c) Copyright Cleverous 2023. All rights reserved.

using UnityEngine;
using UnityEngine.EventSystems;

namespace Cleverous.VaultInventory.Example
{
    /// <summary>
    /// <para>This is an example of how to handle referencing the UI for use in other classes, like Player classes, to show/hide UI.</para>
    /// <para>You might choose to do something similar, or use a different approach that suits your requirements better.</para>
    /// </summary>
    public class VaultExampleUiProxy : MonoBehaviour
    {
        public static VaultExampleUiProxy Instance { get; private set; }

        public GameObject WrapperObject;
        public InventoryUi SelectOnShow;
        public bool CharacterPanelIsShown => WrapperObject.activeSelf;

        private void Awake()
        {
            MerchantUi.OnOpened += Show;
            Instance = this;
        }
        private void Start()
        {
            Hide();
        }
        public void Show()
        {
            WrapperObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(SelectOnShow.Slots[0].gameObject);
        }
        public void Hide()
        {
            if (InventoryUi.ClickedItem != null)
            {
                InventoryUi.ClickedItem.SetAsNotEngaged();
                InventoryUi.ClickedItem = null;
            }
            UiContextMenu.Instance.HideContextMenu();
            WrapperObject.SetActive(false);
        }
        public void Toggle()
        {
            if (WrapperObject.activeSelf) Hide();
            else Show();
        }
    }
}