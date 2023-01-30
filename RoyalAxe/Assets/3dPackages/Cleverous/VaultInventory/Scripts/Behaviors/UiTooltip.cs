// (c) Copyright Cleverous 2023. All rights reserved.

using TMPro;
using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// The Tooltip will hook into <see cref="InventoryUi"/> to display information on <see cref="RootItemStack"/>s in the <see cref="Inventory"/>.
    /// </summary>
    public class UiTooltip : MonoBehaviour
    {
        public static UiTooltip Instance;
        public TMP_Text Title;
        public TMP_Text Description;

        // TODO force tooltip window to framed within the bounds of screen space, using edges as bumpers

        private RectTransform m_tooltipRect;

        protected virtual void Awake()
        {
            MerchantUi.OnClosed += Hide;
            InventoryUi.OnAnyInventoryUiClosed += Hide;
            UiContextMenu.OnOpened += Hide;

            m_tooltipRect = GetComponent<RectTransform>();
            Instance = this;
            Hide();
        }

        public virtual void Show(RectTransform rt, string title, string description)
        {
            if (rt == null) return;

            Title.text = title;
            Description.text = description;
            transform.position = rt.position - new Vector3
            {
                x = rt.rect.width / 2,
                y = rt.rect.height / 2
            };

            gameObject.SetActive(true);
        }

        public virtual void Show(ItemUiPlug plug)
        {
            m_tooltipRect.SetAsLastSibling();
            if (plug == null || plug.GetReferenceVaultItemData() == null) return;
            RectTransform rt = plug.GetComponent<RectTransform>();
            
            Show(rt, 
                plug.GetReferenceVaultItemData().GetUiTitle(), 
                plug.GetReferenceVaultItemData().GetDescriptionComplex());
        }

        public virtual void Hide()
        {
            Title.text = "";
            Description.text = "";
            gameObject.SetActive(false);
        }
    }
}